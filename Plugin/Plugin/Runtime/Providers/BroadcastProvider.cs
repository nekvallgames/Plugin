using Photon.Hive.Plugin;
using System.Collections.Generic;

namespace Plugin.Runtime.Providers
{
    /// <summary>
    /// Сервіс, за допомогою котрого будемо відправляти повідомлення акторам ігрової кімнати
    /// </summary>
    public class BroadcastProvider
    {
        private PluginBase _pluginBase;
        
        public BroadcastProvider(PluginBase pluginBase)
        {       
            _pluginBase = pluginBase;
        }

        /// <summary>
        /// Game server send operation to client or clients
        /// </summary>
        public void Send(byte target, int senderActor, byte targetGroup, byte evCode, Dictionary<byte, object> data, byte cacheOp, SendParameters sendParameters = default)
        {
            _pluginBase.PluginHost.BroadcastEvent(target,          // отправить сообщение всем
                                                  senderActor,     // номер актера, если нужно отправить уникальное сообщение
                                                  targetGroup,
                                                  evCode,
                                                  data,
                                                  cacheOp);        // не кэшировать сообщение
        }

        /// <summary>
        /// Game server send operation to client or clients
        /// </summary>
        public void Send(IList<int> recieverActors, int senderActor, byte evCode, Dictionary<byte, object> data, byte cacheOp, SendParameters sendParameters = default)
        {
            _pluginBase.PluginHost.BroadcastEvent(recieverActors,                  // отправить сообщение списку участников
                                                  senderActor,                     // кто отправил
                                                  evCode,
                                                  data,
                                                  cacheOp); // не кэшировать сообщение
        }
    }
}
