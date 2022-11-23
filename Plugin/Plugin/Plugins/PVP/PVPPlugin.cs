using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Plugins.PVP.States;
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
            gameInstaller.pushService = new BroadcastService(this);  // TODO не подобається как було реалізовано прокидання єкземпляра PluginBase до классу PushService



            gameInstaller.plotService.Add(new IState[] { new AccumulateState(2, SyncChoosedUnitsState.NAME),
                                                         new SyncChoosedUnitsState(2, "")});


            gameInstaller.plotService.ChangeState(AccumulateState.NAME);
        }


        
    }
}
