using Photon.Hive.Plugin;
using System.Collections.Generic;

namespace Plugin.Runtime.Services
{
    public class BroadcastService
    {
        private PluginBase _pluginBase;

        public BroadcastService(PluginBase pluginBase)
        {
            _pluginBase = pluginBase;
        }

        /// <summary>
        /// Game server send operation to client or clients
        /// </summary>
        public void Push(byte target, int senderActor, byte targetGroup, byte evCode, Dictionary<byte, object> data, byte cacheOp, SendParameters sendParameters = default)
        {
            _pluginBase.PluginHost.BroadcastEvent(target,          // отправить сообщение всем
                                                  senderActor,     // номер актера, если нужно отправить уникальное сообщение
                                                  targetGroup,
                                                  evCode,
                                                  data,
                                                  cacheOp);        // не кэшировать сообщение
        }
    }
}
