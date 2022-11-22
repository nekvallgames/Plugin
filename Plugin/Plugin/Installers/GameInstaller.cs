using Plugin.Builders;
using Plugin.Interfaces;
using Plugin.Models.Private;
using Plugin.Models.Public;
using Plugin.Runtime.Providers;
using Plugin.Runtime.Services;
using Plugin.Runtime.Services.ExecuteAction;
using Plugin.Runtime.Services.ExecuteAction.Action;
using Plugin.Runtime.Services.ExecuteAction.Additional;
using Plugin.Runtime.Services.Sync;
using Plugin.Schemes;
using System.Collections.Generic;

namespace Plugin.Installers
{
    public class GameInstaller
    {
        private static GameInstaller _instance;
        public static GameInstaller GetInstance(){
            if (_instance == null){
                _instance = new GameInstaller();
            }
            return _instance;
        }

        public PlotService plotService;
        public DeserializeOpService deserializeOpService;
        public SignalBus signalBus;
        public UnitsService unitsService;
        public SortOpStepService sortOpStepService;
        public SyncService syncService;
        public ExecuteMoveService executeMoveService;
        public ExecuteVipService executeVipService;
        public ExecuteActionService executeActionService;
        public SortTargetOnGridService sortTargetOnGridService;
        public ExecuteAdditionalService executeAdditionalService;
        public ActorsService actorsService;
        public OpStockService opStockService;
        public BroadcastService pushService;
        public GridService gridService;
        public PublicModelProvider publicModelProvider;
        public PrivateModelProvider privateModelProvider;
        public GridBuilder gridBuilder;

        public GameInstaller()
        {
            _instance = this;

            InstallBuilders();
            InstallProviders();
            InstallServices();
        }

        private void InstallBuilders()
        {
            gridBuilder = new GridBuilder();
        }

        private void InstallProviders()
        {
            publicModelProvider = new PublicModelProvider(new List<IPublicModel> 
            {
                new LocationsPublicModel<LocationScheme>() 
            });

            privateModelProvider = new PrivateModelProvider(new List<IPrivateModel>
            {
                new PlotStatesPrivateModel(),
                new UnitsPrivateModel<IUnit>(),
                new OpStockPrivateModel<OpScheme>(),
                new SyncPrivateModel<SyncScheme>(),
                new GridsPrivateModel<GridScheme>(),
                new ActorsPrivateModel<ActorScheme>()
            });
        }

        private void InstallServices()
        {
            plotService = new PlotService(privateModelProvider.Get<PlotStatesPrivateModel>());
            signalBus = new SignalBus();
            unitsService = new UnitsService(privateModelProvider.Get<UnitsPrivateModel<IUnit>>());
            deserializeOpService = new DeserializeOpService();
            sortOpStepService = new SortOpStepService();
            syncService = new SyncService(privateModelProvider.Get<SyncPrivateModel<SyncScheme>>());
            executeMoveService = new ExecuteMoveService();
            executeVipService = new ExecuteVipService();
            executeActionService = new ExecuteActionService();
            sortTargetOnGridService = new SortTargetOnGridService();
            executeAdditionalService = new ExecuteAdditionalService();
            actorsService = new ActorsService(privateModelProvider.Get<ActorsPrivateModel<ActorScheme>>());
            opStockService = new OpStockService(privateModelProvider.Get<OpStockPrivateModel<OpScheme>>());
            gridService = new GridService(publicModelProvider, privateModelProvider, signalBus, gridBuilder);
        }
    }
}
