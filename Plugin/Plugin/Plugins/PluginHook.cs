using Photon.Hive.Plugin;
using Plugin.Installers;
using Plugin.Runtime.Services;
using Plugin.Schemes;

namespace Plugin.Plugins
{
    /// <summary>
    /// Клас котрий буде перехвачувати сигнали Game Server
    /// </summary>
    public class PluginHook : PluginBase
    {
        private SignalBus _signalBus;
        private OpStockService _opStockService;
        private ActorsService _actorsService;

        public PluginHook()
        {
            var gameInstaller = GameInstaller.GetInstance();

            _signalBus = gameInstaller.signalBus;
            _opStockService = gameInstaller.opStockService;
            _actorsService = gameInstaller.actorsService;
        }

        /// <summary>
        /// Игрок на стороне клиента отправил ивент "Создать комнату" и при успешном создании комнаты на 
        /// стороне GameServer выполнится текущий метод
        /// </summary>
        public override void OnCreateGame(ICreateGameCallInfo info)
        {
            int actorId = !info.IsJoin ? 1 : info.Request.ActorNr;
            _actorsService.CreateActor(info.UserId, actorId);

            info.Continue();
        }

        /// <summary>
        /// Игрок на стороне клиента отправил ивент "Присоединится к текущей комнате" и при успешном 
        /// присоеденении к комнате на стороне GameServer выполнится текущий метод
        /// </summary>
        public override void OnJoin(IJoinGameCallInfo info)
        {
            _actorsService.CreateActor(info.UserId, info.ActorNr);

            info.Continue();
        }

        /// <summary>
        /// Игрок на стороне клиента отправил какой то ивент
        /// </summary>
        public override void OnRaiseEvent(IRaiseEventCallInfo info)
        {
            _opStockService.Add(new OpScheme(info.ActorNr,
                                             info.Request.EvCode,
                                             info.Request.Data));

            info.Continue();
        }

        /// <summary>
        /// Игрок покинул игровую комнаты на стороне GAME SERVER
        /// </summary>
        public override void OnLeave(ILeaveGameCallInfo info)
        {
            _actorsService.EnableConnected(info.ActorNr, false);

            info.Continue();
        }
    }
}
