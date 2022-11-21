using Photon.Hive.Plugin;
using Plugin.Installers;
using Plugin.Models.Private;
using Plugin.Runtime.Services;
using Plugin.Schemes;
using Plugin.Signals;

namespace Plugin.Plugins
{
    /// <summary>
    /// Клас обсервер, котрий буде перехвачувати сигнали Game Server
    /// </summary>
    public class PluginObserver : PluginBase
    {
        private SignalBus _signalBus;
        private OpStockPrivateModel<OpScheme> _opStockPrivateModel;

        public PluginObserver()
        {
            var gameInstaller = GameInstaller.GetInstance();

            _signalBus = gameInstaller.signalBus;
            _opStockPrivateModel = gameInstaller.opStockPrivateModel;
        }

        /// <summary>
        /// Игрок на стороне клиента отправил ивент "Создать комнату" и при успешном создании комнаты на 
        /// стороне GameServer выполнится текущий метод
        /// </summary>
        public override void OnCreateGame(ICreateGameCallInfo info)
        {
            info.Continue();
        }


        /// <summary>
        /// Игрок на стороне клиента отправил какой то ивент
        /// </summary>
        public override void OnRaiseEvent(IRaiseEventCallInfo info)
        {
            _opStockPrivateModel.Add(new OpScheme(info.ActorNr, 
                                                  info.Request.EvCode, 
                                                  info.Request.Data));

            info.Continue();
        }


        /// <summary>
        /// Игрок на стороне клиента отправил ивент "Присоединится к текущей комнате" и при успешном 
        /// присоеденении к комнате на стороне GameServer выполнится текущий метод
        /// </summary>
        public override void OnJoin(IJoinGameCallInfo info)
        {
            // Создать сигнал, что какой то игрок подключился к игре
            _signalBus.Invoke(new OpJoinSignal());

            info.Continue();
        }

        /// <summary>
        /// Игрок покинул игровую комнаты на стороне GAME SERVER
        /// </summary>
        public override void OnLeave(ILeaveGameCallInfo info)
        {
            // Создать сигнал, что какой то игрок покинул игру
            _signalBus.Invoke(new OpLeaveSignal());

            info.Continue();
        }
    }
}
