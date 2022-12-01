using Photon.Hive.Plugin;

namespace Plugin.Signals
{
    /// <summary>
    /// Подія, коли модель із даними хостів ігрових кімнат була змінена
    /// </summary>
    public class HostsPrivateModelSignal : ModelChangeSignal
    {
        public IPluginHost host;

        public HostsPrivateModelSignal(IPluginHost host, StatusType status) : base(status)
        {
            this.host = host;
        }
    }
}
