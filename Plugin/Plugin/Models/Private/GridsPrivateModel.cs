using Plugin.Interfaces;
using Plugin.Runtime.Services;
using Plugin.Signals;
using Plugin.Templates;

namespace Plugin.Models.Private
{
    public class GridsPrivateModel<T> : BaseModel<T>, IPrivateModel where T : IGrid
    {
        private SignalBus _signalBus;

        public GridsPrivateModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        protected override void AfterAddHook(T item)
        {
            _signalBus.Fire(new GridsPrivateModelSignal(item.GameId, item.OwnerActorId, GridsPrivateModelSignal.StatusType.add));
        }

        protected override void AfterRemoveHook(T item)
        {
            _signalBus.Fire(new GridsPrivateModelSignal(item.GameId, item.OwnerActorId, GridsPrivateModelSignal.StatusType.remove));
        }
    }
}
