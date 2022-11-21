using Plugin.Installers;
using Plugin.Runtime.Services;
using Plugin.Signals;
using Plugin.Templates;

namespace Plugin.Models.Private
{
    /// <summary>
    /// Модель із даними, котра зберігає в собі операції, котрі клієнти присилають to Game Server
    /// </summary>
    public class OpStockPrivateModel<T> : BaseModel<T>
    {
        private SignalBus _signalBus;

        public OpStockPrivateModel()
        {
            _signalBus = GameInstaller.GetInstance().signalBus;
        }

        protected override void AfterAddHook(T item)
        {
            var signal = new OpPutOnStockSignal();
            _signalBus.Invoke(signal);
        }
    }
}
