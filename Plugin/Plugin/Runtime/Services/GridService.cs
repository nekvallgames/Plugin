using Plugin.Builders;
using Plugin.Models.Private;
using Plugin.Models.Public;
using Plugin.Runtime.Providers;
using Plugin.Schemes;
using Plugin.Signals;
using System.Linq;

namespace Plugin.Runtime.Services
{
    public class GridService
    {
        private LocationsPublicModel<LocationScheme> _locationsPublicModel;
        private GridsPrivateModel<GridScheme> _gridsPrivateModel;
        private GridBuilder _gridBuilder;

        public GridService(PublicModelProvider publicModelProvider, 
                           PrivateModelProvider privateModelProvider, 
                           SignalBus signalBus,
                           GridBuilder gridBuilder)
        {
            _locationsPublicModel = publicModelProvider.Get<LocationsPublicModel<LocationScheme>>();
            _gridsPrivateModel = privateModelProvider.Get<GridsPrivateModel<GridScheme>>();
            _gridBuilder = gridBuilder;

            signalBus.Subscrible<ActorsPrivateModelSignal>(OnActorsModelChange);
        }

        /// <summary>
        /// Модель із даними гравців була оновлена
        /// </summary>
        private void OnActorsModelChange( ActorsPrivateModelSignal signalData )
        {
            if (_gridsPrivateModel.Items.Any(x => x.ownerActorId == signalData.ActorId))
                return; // для поточного гравця вже створена ігрова сітка

            // Створити ігрову сітку для поточного гравця
            LocationScheme scheme = _locationsPublicModel.Items[0]; // TODO поки що постійно створюємо локацію за замовчуванням

            GridScheme grid = _gridBuilder.Create(signalData.ActorId, scheme.SizeGrid, scheme.GridMask);

            _gridsPrivateModel.Add(grid);

        }
    }
}
