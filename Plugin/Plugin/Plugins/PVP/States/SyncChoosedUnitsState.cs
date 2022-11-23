using Photon.Hive.Plugin;
using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Runtime.Services;
using Plugin.Signals;
using Plugin.Tools;

namespace Plugin.Plugins.PVP.States
{
    /// <summary>
    /// Стейт, в котрому потрібно між клієнтами синхронізувати вибраних юнітів
    /// </summary>
    public class SyncChoosedUnitsState : IState
    {
        public const string NAME = "SyncChoosedUnitsState";
        public string Name => NAME;

        private SignalBus _signalBus;
        private BroadcastService _pushService;
        private OpStockService _opStockService;
        private PlotService _plotService;

        /// <summary>
        /// Кількість гравців, котрі потрібні для старту ігрової кімнати
        /// </summary>
        private int _countActors;
        /// <summary>
        /// Наступний стейт, в котрий перейдемо після поточного стейту
        /// </summary>
        private string _nextState;

        public SyncChoosedUnitsState(int countActors, string nextState)
        {
            _countActors = countActors;
            _nextState = nextState;

            var gameInstaller = GameInstaller.GetInstance();

            _signalBus = gameInstaller.signalBus;
            _pushService = gameInstaller.pushService;
            _opStockService = gameInstaller.opStockService;
            _plotService = gameInstaller.plotService;
        }

        public void EnterState()
        {
            LogChannel.Log("PlotService :: SyncChoosedUnitsState :: EnterState()", LogChannel.Type.Plot);

            // Подписываемся на сигнал о том, что клиент прислал операцию на GameServer
            _signalBus.Subscrible<OpStockPrivateModelSignal>( OnOpStockChange );

            // Отправляем всем актерам операцию, что бы они выбрали юнитов
            _pushService.Push(ReciverGroup.All,                   // отправить сообщение всем
                              0,                                  // номер актера, если нужно отправить уникальное сообщение
                              0,
                              OperationCode.selectUnitsForGame,
                              null,
                              CacheOperations.DoNotCache);        // не кэшировать сообщение
        }

        /// <summary>
        /// Клиент прислал какую то операцию на GameServer
        /// </summary>
        private void OnOpStockChange(OpStockPrivateModelSignal signalData)
        {
            int opStepCount = _opStockService.GetOpCount(OperationCode.choosedUnitsForGame);

            if (opStepCount >= _countActors){
                // Все игроки прислали своих выбранных юнитов
                _plotService.ChangeState(_nextState);
            }
        }

        public void ExitState()
        {
            _signalBus.Unsubscrible<OpStockPrivateModelSignal>(OnOpStockChange);
        }
    }
}
