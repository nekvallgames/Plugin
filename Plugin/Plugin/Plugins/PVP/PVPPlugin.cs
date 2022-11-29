using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Models.Private;
using Plugin.Plugins.PVP.States;
using Plugin.Schemes;

namespace Plugin.Plugins.PVP
{
    /// <summary>
    /// Плагин для режиму PVP
    /// </summary>
    public class PVPPlugin : PluginHook
    {
        public const string NAME = "PVPPlugin";

        /// <summary>
        /// Имя плагина. Что бы текущий плагин выполнял логику созданой комнаты, 
        /// нужно имя плагина передать в параметрах, при создании комнаты
        /// </summary>
        public override string Name => NAME; // anything other than "Default" or "ErrorPlugin"


        public PVPPlugin()
        {
            var gameInstaller = GameInstaller.GetInstance();

            // створити схему, котра буде зберігати в собі данні ігрового сценарія
            gameInstaller.privateModelProvider.Get<PlotsPrivateModel<IPlotModelScheme>>().Add(new PVPPlotModelScheme());
                        
            // створити стейти ігрового сценарія
            gameInstaller.plotService.Add(new IState[] { new AccumulateState(2, SyncStartState.NAME),
                                                         new SyncStartState(2, WaitStepResult.NAME), 
                                                         new WaitStepResult(2, StepResult.NAME),
                                                         new StepResult(WaitStepResult.NAME)
                                                        });

            // запустити ігровий сценарій
            gameInstaller.plotService.ChangeState(AccumulateState.NAME);
        }


        
    }
}
