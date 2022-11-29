using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Runtime.Services;
using Plugin.Schemes;
using Plugin.Signals;
using Plugin.Templates;

namespace Plugin.Models.Private
{
    /// <summary>
    /// Приватна модель із даними, котра зберігає в собі акторів
    /// </summary>
    public class ActorsPrivateModel<T> : BaseModel<T>, IPrivateModel where T : ActorScheme
    {
        private SignalBus _signalBus;

        public ActorsPrivateModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        /// <summary>
        /// Виконається після добавлення поточного єлементу до списку
        /// </summary>
        protected override void AfterAddHook(T item) 
        {
            _signalBus.Fire(new ActorsPrivateModelSignal(item.ActorId, ModelChangeSignal.StatusType.add));
        }
    }
}
