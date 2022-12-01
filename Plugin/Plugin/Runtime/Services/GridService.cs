using Photon.Hive.Plugin;
using Plugin.Builders;
using Plugin.Interfaces;
using Plugin.Models.Private;
using Plugin.Models.Public;
using Plugin.Runtime.Providers;
using Plugin.Schemes;
using Plugin.Signals;
using System.Linq;

namespace Plugin.Runtime.Services
{
    /// <summary>
    /// Сервіс, котрий реалізує логіку ігрової сітки для акторів
    /// </summary>
    public class GridService
    {
        private LocationsPublicModel<LocationScheme> _locationsPublicModel;
        private GridsPrivateModel<IGrid> _gridsPrivateModel;
        private GridBuilder _gridBuilder;

        public GridService(PublicModelProvider publicModelProvider, 
                           PrivateModelProvider privateModelProvider, 
                           GridBuilder gridBuilder, 
                           SignalBus signalBus)
        {
            _locationsPublicModel = publicModelProvider.Get<LocationsPublicModel<LocationScheme>>();
            _gridsPrivateModel = privateModelProvider.Get<GridsPrivateModel<IGrid>>();
            _gridBuilder = gridBuilder;

            signalBus.Subscrible<HostsPrivateModelSignal>(HostsModelChange);
        }

        /// <summary>
        /// Модель із даними хостів була змінена
        /// </summary>
        private void HostsModelChange(HostsPrivateModelSignal signalData )
        {
            foreach ( IActor actor in signalData.host.GameActorsActive )
            {
                if (_gridsPrivateModel.Items.Any(x => x.OwnerActorId == actor.ActorNr))
                    continue;   // для поточного гравця вже створена ігрова сітка

                // Створити ігрову сітку для поточного гравця
                LocationScheme scheme = _locationsPublicModel.Items[0]; // TODO поки що постійно створюємо локацію за замовчуванням

                IGrid grid = _gridBuilder.Create(actor.ActorNr, scheme.SizeGrid, scheme.GridMask);

                _gridsPrivateModel.Add(grid);
            }
        }
    }
}
