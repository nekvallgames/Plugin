using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Runtime.Services;
using Plugin.Signals;
using Plugin.Tools;

namespace Plugin.Plugins.PVP.States
{
    /// <summary>
    /// Стейт, в котрому чекаємо, поки кімната збере необхідну кількість гравців
    /// </summary>
    public class AccumulateState : IState
    {
        public const string NAME = "AccumulateState";
        public string Name => NAME;

        private SignalBus _signalBus;
        private ActorsService _actorsService;
        private PlotService _plotService;

        /// <summary>
        /// Кількість гравців, котрі потрібні для старту ігрової кімнати
        /// </summary>
        private int _countActors;
        /// <summary>
        /// Наступний стейт, в котрий перейдемо після поточного стейту
        /// </summary>
        private string _nextState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="countActors">кількість гравців, котрі потрібні для старту ігрової кімнати</param>
        /// <param name="nextState">перейти в вказаний стейт</param>
        public AccumulateState(int countActors, string nextState)
        {
            _countActors = countActors;
            _nextState = nextState;


            var gameInstaller = GameInstaller.GetInstance();

            _signalBus = gameInstaller.signalBus;
            _actorsService = gameInstaller.actorsService;
            _plotService = gameInstaller.plotService;
        }

        public void EnterState()
        {
            LogChannel.Log("PlotService :: AccumulateState :: EnterState()", LogChannel.Type.Plot);

            _signalBus.Subscrible<ActorsPrivateModelSignal>(OnChangeActorModel);
        }

        /// <summary>
        /// Подія, коли модель із данними гравців була оновлена
        /// </summary>
        private void OnChangeActorModel( ActorsPrivateModelSignal signalData )
        {
            if (_actorsService.GetConnectedActors().Count >= _countActors){
                _plotService.ChangeState(_nextState);
            }
        }

        public void ExitState()
        {
            _signalBus.Unsubscrible<ActorsPrivateModelSignal>(OnChangeActorModel);
        }
    }
}
