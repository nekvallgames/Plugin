using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Runtime.Services;
using Plugin.Signals;
using Plugin.Tools;

namespace Plugin.Plugins.PVP.States
{
    /// <summary>
    /// Состояние, в котором мы ждем, когда игроки пришлют свой шаг действия
    /// </summary>
    public class WaitStepResult : IState
    {
        public const string NAME = "WaitStepResult";
        public string Name => NAME;

        private SignalBus _signalBus;
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

        public WaitStepResult(int countActors, string nextState)
        {
            _countActors = countActors;
            _nextState = nextState;

            var gameInstaller = GameInstaller.GetInstance();

            _signalBus = gameInstaller.signalBus;
            _opStockService = gameInstaller.opStockService;
            _plotService = gameInstaller.plotService;
        }

        public void EnterState()
        {
            LogChannel.Log("PlotService :: WaitStepResult :: EnterState()", LogChannel.Type.Plot);

            // Слушаем сигнал о том, что клиент прислал операцию на GameServer
            _signalBus.Subscrible<OpStockPrivateModelSignal>(OpStockModelChanged);

            // TODO в майбутньому додати таймер. У гравців на виконання кроку є обмежений час
        }

        /// <summary>
        /// Модель із операціями гравця була змінена
        /// </summary>
        private void OpStockModelChanged(OpStockPrivateModelSignal signalData)
        {
            int opStepCount = _opStockService.GetOpCount(OperationCode.actorStep);

            if (opStepCount == _countActors)
            {
                // Оба игрока прислали свой ход. Можно не ждать окончание таймера, 
                // а перейти в следующее состояние
                _plotService.ChangeState(_nextState);
            }
        }

        public void ExitState()
        {
            _signalBus.Unsubscrible<OpStockPrivateModelSignal>(OpStockModelChanged);
        }
    }
}
