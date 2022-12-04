using Photon.Hive.Plugin;
using Plugin.Interfaces;
using Plugin.Runtime.Services;
using Plugin.Signals;
using Plugin.Templates;

namespace Plugin.Models.Private
{
    public class HostsPrivateModel<T> : BaseModel<T>, IPrivateModel where T : IPluginHost
    {
        private SignalBus _signalBus;

        public HostsPrivateModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        protected override void AfterAddHook(T item)
        {
            _signalBus.Fire(new HostsPrivateModelSignal(item.GameId, HostsPrivateModelSignal.StatusType.add));
        }

        protected override void AfterRemoveHook(T item)
        {
            _signalBus.Fire(new HostsPrivateModelSignal(item.GameId, HostsPrivateModelSignal.StatusType.remove));
        }
    }
}
