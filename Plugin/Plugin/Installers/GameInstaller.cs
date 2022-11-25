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
        public SignalBus signalBus;
        public UnitsService unitsService;
        public SortOpStepService sortOpStepService;
        public SyncService syncService;
        public MoveService executeMoveService;
        public VipService executeVipService;
        public ActionService executeActionService;
        public SortTargetOnGridService sortTargetOnGridService;
        public ExecuteAdditionalService executeAdditionalService;
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

        public GameInstaller()
        {
            _instance = this;

            signalBus = new SignalBus();
            convertService = new ConvertService();
            sortOpStepService = new SortOpStepService();

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
                new ActorsPrivateModel<ActorScheme>(signalBus)
            });

            gridBuilder = new GridBuilder();
            
            unitInstanceService = new UnitInstanceService(privateModelProvider.Get<UnitsPrivateModel<IUnit>>());
            unitBuilder = new UnitBuilder(unitInstanceService);
            opStockService = new OpStockService(privateModelProvider.Get<OpStockPrivateModel<IOpStockItem>>());
            unitsService = new UnitsService(privateModelProvider.Get<UnitsPrivateModel<IUnit>>(), opStockService, convertService, unitBuilder, signalBus);
            syncService = new SyncService(privateModelProvider.Get<SyncPrivateModel<SyncScheme>>());
            executeMoveService = new MoveService(syncService);
            plotService = new PlotService(privateModelProvider.Get<PlotStatesPrivateModel>());
            executeVipService = new VipService(syncService, unitsService);
            sortTargetOnGridService = new SortTargetOnGridService();
            executeActionService = new ActionService(syncService, unitsService, sortTargetOnGridService);
            executeAdditionalService = new ExecuteAdditionalService(syncService, unitsService);
            actorsService = new ActorsService(privateModelProvider.Get<ActorsPrivateModel<ActorScheme>>(), signalBus);
            gridService = new GridService(publicModelProvider, privateModelProvider, gridBuilder, signalBus);
            broadcastProvider = new BroadcastProvider(PluginHook.Instance);
            notificationChangeVipService = new NotificationChangeVipService(opStockService, actorsService, signalBus, broadcastProvider);
        }
    }
}
