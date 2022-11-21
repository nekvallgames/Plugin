using Plugin.Interfaces;
using Plugin.Models.Private;
using Plugin.Runtime.Services;
using Plugin.Runtime.Services.ExecuteAction;
using Plugin.Runtime.Services.ExecuteAction.Action;
using Plugin.Runtime.Services.ExecuteAction.Additional;
using Plugin.Runtime.Services.Sync;
using Plugin.Schemes;

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
        public OpStockPrivateModel<OpScheme> opStockPrivateModel;
        public GridsPrivateModel<GridScheme> gridsPrivateModel;
        public SortOpStepService sortOpStepService;
        public SyncService syncService;
        public ExecuteMoveService executeMoveService;
        public ExecuteVipService executeVipService;
        public ExecuteActionService executeActionService;
        public SortTargetOnGridService sortTargetOnGridService;
        public ExecuteAdditionalService executeAdditionalService;

        private UnitsPrivateModel<IUnit> _unitsPrivateModel;
        private SyncPrivateModel<SyncScheme> _syncPrivateModel;
        private PlotStatesPrivateModel _plotStatesPrivateModel;


        public GameInstaller()
        {
            _instance = this;

            InstallModels();
            InstallServices();
        }

        private void InstallModels()
        {
            _plotStatesPrivateModel = new PlotStatesPrivateModel();
            _unitsPrivateModel = new UnitsPrivateModel<IUnit>();
            opStockPrivateModel = new OpStockPrivateModel<OpScheme>();
            _syncPrivateModel = new SyncPrivateModel<SyncScheme>();
        }

        private void InstallServices()
        {
            plotService = new PlotService(_plotStatesPrivateModel);
            signalBus = new SignalBus();
            unitsService = new UnitsService(_unitsPrivateModel);
            deserializeOpService = new DeserializeOpService();
            sortOpStepService = new SortOpStepService();
            syncService = new SyncService(_syncPrivateModel);
            executeMoveService = new ExecuteMoveService();
            executeVipService = new ExecuteVipService();
            executeActionService = new ExecuteActionService();
            sortTargetOnGridService = new SortTargetOnGridService();
            executeAdditionalService = new ExecuteAdditionalService();
        }
    }
}
