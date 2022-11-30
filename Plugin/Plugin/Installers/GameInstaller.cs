using Plugin.Builders;
using Plugin.Interfaces;
using Plugin.Models.Private;
using Plugin.Models.Public;
using Plugin.Plugins;
using Plugin.Runtime.Providers;
using Plugin.Runtime.Services;
using Plugin.Runtime.Services.ExecuteAction;
using Plugin.Runtime.Services.ExecuteAction.Action;
using Plugin.Runtime.Services.ExecuteAction.Additional;
using Plugin.Runtime.Services.ExecuteOp;
using Plugin.Runtime.Services.Sync;
using Plugin.Runtime.Spawners;
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
        public SignalBus signalBus;
        public UnitsService unitsService;
        public SyncService syncService;
        public MoveService moveService;
        public VipService vipService;
        public ActionService actionService;
        public SortTargetOnGridService sortTargetOnGridService;
        public AdditionalService additionalService;
        public ActorsService actorsService;
        public OpStockService opStockService;
        public BroadcastProvider broadcastProvider;
        public GridService gridService;
        public PublicModelProvider publicModelProvider;
        public PrivateModelProvider privateModelProvider;
        public GridBuilder gridBuilder;
        public ConvertService convertService;
        public UnitInstanceService unitInstanceService;
        public UnitBuilder unitBuilder;
        public NotificationChangeVipService notificationChangeVipService;
        public StepSchemeBuilder stepSchemeBuilder;
        public LocationUnitsSpawner locationUnitsSpawner;
        public SyncStepService syncStepService;

        public ExecuteOpStepSchemeService executeOpStepService;
        public ExecuteOpGroupService executeOpGroupService;


        public GameInstaller()
        {
            _instance = this;

            signalBus = new SignalBus();
            convertService = new ConvertService();

            publicModelProvider = new PublicModelProvider(new List<IPublicModel>
            {
                new LocationsPublicModel<LocationScheme>(convertService)
            });

            privateModelProvider = new PrivateModelProvider(new List<IPrivateModel>
            {
                new PlotStatesPrivateModel(),
                new UnitsPrivateModel<IUnit>(),
                new OpStockPrivateModel<IOpStockItem>(signalBus),
                new SyncPrivateModel<SyncScheme>(),
                new GridsPrivateModel<GridScheme>(),
                new ActorsPrivateModel<ActorScheme>(signalBus),
                new PlotsPrivateModel<IPlotModelScheme>()
            });

            gridBuilder = new GridBuilder();
            unitInstanceService = new UnitInstanceService(privateModelProvider.Get<UnitsPrivateModel<IUnit>>());
            unitBuilder = new UnitBuilder(unitInstanceService);
            opStockService = new OpStockService(privateModelProvider.Get<OpStockPrivateModel<IOpStockItem>>());
            syncService = new SyncService(privateModelProvider.Get<SyncPrivateModel<SyncScheme>>(), privateModelProvider.Get< PlotsPrivateModel<IPlotModelScheme>>());
            stepSchemeBuilder = new StepSchemeBuilder(syncService);
            moveService = new MoveService(syncService);
            unitsService = new UnitsService(privateModelProvider.Get<UnitsPrivateModel<IUnit>>(), opStockService, convertService, unitBuilder, signalBus, moveService);
            locationUnitsSpawner = new LocationUnitsSpawner(publicModelProvider, unitsService, signalBus);
            plotService = new PlotService(privateModelProvider.Get<PlotStatesPrivateModel>());
            vipService = new VipService(syncService, unitsService);
            sortTargetOnGridService = new SortTargetOnGridService();
            actionService = new ActionService(syncService, unitsService, sortTargetOnGridService);
            additionalService = new AdditionalService(syncService, unitsService);
            actorsService = new ActorsService(privateModelProvider.Get<ActorsPrivateModel<ActorScheme>>(), signalBus);
            gridService = new GridService(publicModelProvider, privateModelProvider, gridBuilder, signalBus);
            broadcastProvider = new BroadcastProvider(PluginHook.Instance);
            syncStepService = new SyncStepService(broadcastProvider, actorsService, stepSchemeBuilder, convertService);
            notificationChangeVipService = new NotificationChangeVipService(opStockService, actorsService, signalBus, broadcastProvider);
            executeOpGroupService = new ExecuteOpGroupService(unitsService, moveService, vipService, actionService, additionalService);
            executeOpStepService = new ExecuteOpStepSchemeService(executeOpGroupService);

            
        }
    }
}
