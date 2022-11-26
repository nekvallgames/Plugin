using Photon.Hive.Plugin;
using Plugin.Builders;
using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Models.Private;
using Plugin.Runtime.Providers;
using Plugin.Runtime.Services;
using Plugin.Runtime.Services.ExecuteOp;
using Plugin.Schemes;
using Plugin.Tools;
using System.Collections.Generic;

namespace Plugin.Plugins.PVP.States
{
    /// <summary>
    /// Состояние, при котором мы обрабатываем результат шага игроков
    /// Игроки переставили своих юнитов и атаковали друг друга
    /// 
    /// Нужно сначала синхронизировать первый шаг, где игрок розставляет своих юнитов на игровой сетке
    /// И после выполнить второй шаг - где игрок уже атаковал противника
    /// </summary>
    public class StepResult : IState
    {
        public const string NAME = "StepResult";
        public string Name => NAME;

        private UnitsService _unitsService;
        private ActorsService _actorsService;
        private ConvertService _convertService;
        private OpStockService _opStockService;
        private PVPPlotScheme _plotModel;
        private ExecuteOpStepService _executeOpStepService;
        private StepSchemeBuilder _stepSchemeBuilder;
        private BroadcastProvider _broadcastProvider;
        private PlotService _plotService;

        private string _nextStep;

        public StepResult(string nextStep)
        {
            _nextStep = nextStep;

            var gameInstaller = GameInstaller.GetInstance();

            _unitsService = gameInstaller.unitsService;
            _actorsService = gameInstaller.actorsService;
            _convertService = gameInstaller.convertService;
            _opStockService = gameInstaller.opStockService;
            _executeOpStepService = gameInstaller.executeOpStepService;
            _stepSchemeBuilder = gameInstaller.stepSchemeBuilder;
            _broadcastProvider = gameInstaller.broadcastProvider;
            _plotService = gameInstaller.plotService;

            var plotsPrivateModel = gameInstaller.privateModelProvider.Get<PlotsPrivateModel<IPlotScheme>>();
            _plotModel = plotsPrivateModel.Items[0] as PVPPlotScheme;
        }

        public void EnterState()
        {
            LogChannel.Log("PlotService :: PVPStepResult :: EnterState()", LogChannel.Type.Plot);

            // Всим юнітам акторів перезарядити їхні єкшени
            foreach (ActorScheme sctor in _actorsService.Actors){
                _unitsService.ReviveAction(sctor.ActorId);
            }

            var actorSteps = new List<ActorStep>();
            DeserializeOp(ref actorSteps);

            // Step - move
            ExecuteSteps(ref actorSteps, _plotModel.SyncStep);     
            _plotModel.SyncStep++;

            // Step - attack
            ExecuteSteps(ref actorSteps, _plotModel.SyncStep);     
            _plotModel.SyncStep++;

            // Создать коллекцию, которая будет хранить в себе данные, которые нужно синхронизировать 
            // между клиентами
            // key   - это ActorID
            // value - это StepScheme, которая имеет кучу из компонентов
            var pushData = new Dictionary<byte, object> { };

            // Зібрати синхронізацію дій акторів і відправити результат їхній дій всім акторам в кімнаті
            foreach (ActorScheme actor in _actorsService.Actors)
            {
                StepScheme scheme = _stepSchemeBuilder.Create(actor.ActorId, new int[] { _plotModel.SyncStep - 2, _plotModel.SyncStep - 1 });
                string jsonString = _convertService.SerializeObject(scheme);
                pushData.Add((byte)actor.ActorId, jsonString);
            }

            _broadcastProvider.Send(ReciverGroup.All,                   // отправить сообщение всем
                                    0,                                  // номер актера, если нужно отправить уникальное сообщение
                                    0,
                                    OperationCode.stepResult,
                                    pushData,
                                    CacheOperations.DoNotCache);        // не кэшировать сообщение

            _plotService.ChangeState(_nextStep);
        }

        /// <summary>
        /// Нужно десериализировать операции, которые прислал игрок на Game Server
        /// </summary>
        private void DeserializeOp(ref List<ActorStep> actorSteps)
        {
            foreach (ActorScheme actor in _actorsService.Actors)
            {
                if (!_opStockService.HasOp(actor.ActorId, OperationCode.actorStep))
                    continue;

                var stepData = _opStockService.TakeOp(actor.ActorId, OperationCode.actorStep);

                var stepScheme = _convertService.DeserializeObject<StepScheme>(stepData.Data.ToString());

                actorSteps.Add(new ActorStep(actor.ActorId, stepScheme));
            }
        }

        private void ExecuteSteps(ref List<ActorStep> actorSteps, int syncStep)
        {
            foreach (ActorStep actorStep in actorSteps){
                _executeOpStepService.Execute(actorStep.actorId, syncStep, actorStep.stepScheme);
            }
        }

        public void ExitState()
        {
            
        }
    }

    /// <summary>
    /// Класс, который будет хранить в себе данные с результатами действий игрока
    /// </summary>
    internal struct ActorStep
    {
        /// <summary>
        /// ID игрока
        /// </summary>
        public int actorId;

        /// <summary>
        /// Схема со всеми действиями игрока
        /// Куча компонентов, которые разсортированы по спискам
        /// </summary>
        public StepScheme stepScheme;

        public ActorStep(int actorId, StepScheme stepScheme)
        {
            this.actorId = actorId;
            this.stepScheme = stepScheme;
        }
    }
}
