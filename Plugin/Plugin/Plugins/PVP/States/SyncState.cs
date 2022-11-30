using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Models.Private;
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
    public class SyncState : IState
    {
        public const string NAME = "SyncState";
        public string Name => NAME;

        private UnitsService _unitsService;
        private ActorsService _actorsService;
        private ConvertService _convertService;
        private OpStockService _opStockService;
        private PVPPlotModelScheme _plotModel;
        private ExecuteOpStepSchemeService _executeOpStepService;
        private PlotService _plotService;
        private SyncStepService _syncStepService;

        private string _nextStep;

        public SyncState(string nextStep)
        {
            _nextStep = nextStep;

            var gameInstaller = GameInstaller.GetInstance();

            _unitsService = gameInstaller.unitsService;
            _actorsService = gameInstaller.actorsService;
            _convertService = gameInstaller.convertService;
            _opStockService = gameInstaller.opStockService;
            _executeOpStepService = gameInstaller.executeOpStepService;
            _plotService = gameInstaller.plotService;
            _syncStepService = gameInstaller.syncStepService;

            var plotsPrivateModel = gameInstaller.privateModelProvider.Get<PlotsPrivateModel<IPlotModelScheme>>();
            _plotModel = plotsPrivateModel.Items[0] as PVPPlotModelScheme;
        }

        public void EnterState()
        {
            LogChannel.Log("PlotService :: PVPStepResult :: EnterState()", LogChannel.Type.Plot);

            // Всим юнітам акторів перезарядити їхні єкшени
            foreach (ActorScheme sctor in _actorsService.Actors){
                _unitsService.ReviveAction(sctor.ActorId);
            }

            // Десериализировать операцію StepScheme акторів, котрі вони прислали 
            var actorSteps = new List<ActorStep>();
            DeserializeOp(ref actorSteps);

            // Виконати перший крок - move
            ExecuteSteps(ref actorSteps, _plotModel.SyncStep);     
            _plotModel.SyncStep++;

            // Виконати другий крок - attack
            ExecuteSteps(ref actorSteps, _plotModel.SyncStep);     
            _plotModel.SyncStep++;


            _syncStepService.Sync(new int[] { _plotModel.SyncStep - 2, _plotModel.SyncStep - 1 });

            _plotService.ChangeState(_nextStep);
        }

        /// <summary>
        /// Нужно десериализировать операции, которые прислал игрок на Game Server
        /// </summary>
        private void DeserializeOp(ref List<ActorStep> actorSteps)
        {
            foreach (ActorScheme actor in _actorsService.Actors)
            {
                if (!_opStockService.HasOp(actor.ActorId, OperationCode.syncStep))
                    continue;

                var stepData = _opStockService.TakeOp(actor.ActorId, OperationCode.syncStep);

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
