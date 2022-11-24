using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Plugins.PVP.States;
using Plugin.Runtime.Providers;
using Plugin.Runtime.Services;

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
            gameInstaller.broadcastProvider = new BroadcastProvider(this);  // TODO не подобається как було реалізовано прокидання єкземпляра PluginBase до классу PushService



            gameInstaller.plotService.Add(new IState[] { new AccumulateState(2, SyncStartState.NAME),
                                                         new SyncStartState(2, "")});


            gameInstaller.plotService.ChangeState(AccumulateState.NAME);
        }


        
    }
}
