using Photon.Hive.Plugin;

namespace Plugin.Plugins.PVP
{
    /// <summary>
    /// Плагин для режиму PVP
    /// </summary>
    public class PVPPlugin : PluginObserver
    {
        public const string NAME = "PVPPlugin";

        /// <summary>
        /// Имя плагина. Что бы текущий плагин выполнял логику созданой комнаты, 
        /// нужно имя плагина передать в параметрах, при создании комнаты
        /// </summary>
        public override string Name => NAME; // anything other than "Default" or "ErrorPlugin"


        public PVPPlugin()
        {
           // _operationHub = _gameInstaller.containerControllers.opHubController;
            
           
           // // 3.1. Создать класс, в котором будут хранится всякие данные игры
           // _model = new PVPModel();
           //
           // // 3.2. Создать стейт машину для логики режима игры
           //
           //
           //
           // _stateMachine = new PVPPluginStateMachine(this, _model);
           // _stateMachine.ChangeState(PVPPluginStateMachine.STATE.PreparationRoom);
        }


        
    }
}
