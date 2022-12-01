using Photon.Hive.Plugin;
using Plugin.Models.Private;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Plugin.Runtime.Services
{
    /// <summary>
    /// Сервіс, котрий буде маніпулювати хостами ігрових кімнат
    /// </summary>
    public class HostsService
    {
        private HostsPrivateModel<IPluginHost> _model;

        public HostsService(HostsPrivateModel<IPluginHost> model)
        {
            _model = model;
        }

        public void Add(IPluginHost host)
        {
            _model.Add(host);
        }

        public IPluginHost Get(int actorId)
        {
            return _model.Items.Find(x => x.GameActorsActive.Any(y => y.ActorNr == actorId));
        }

        /// <summary>
        /// Отримати акторів, котрі знаходяться в ігровій кімнаті
        /// </summary>
        public IList<IActor> Actors(IPluginHost host)
        {
            return host.GameActorsActive;
        }

        /// <summary>
        /// Вказаний актор є участником даної кімнати?
        /// </summary>
        public bool IsMemberHost(IPluginHost host, int actorId)
        {
            return host.GameActorsActive.Any(x => x.ActorNr == actorId);
        }
    }
}
