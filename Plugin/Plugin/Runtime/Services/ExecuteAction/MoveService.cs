using Plugin.Installers;
using Plugin.Interfaces.Units;
using Plugin.Runtime.Services.Sync;
using Plugin.Runtime.Services.Sync.Groups;
using Plugin.Tools;

namespace Plugin.Runtime.Services.ExecuteAction
{
    /// <summary>
    /// Сервіс, котрий переміщає та позиціонує юнітів по ігровій сітці
    /// </summary>
    public class MoveService
    {
        private SyncService _syncService;

        public MoveService()
        {
            _syncService = GameInstaller.GetInstance().syncService;
        }

        /// <summary>
        /// Позиционировать юнита в точке posW, posH на игровой сетке,
        /// без каких либо проверок, может юнит туда дойти или нет
        /// </summary>
        public void PositionOnGrid(IUnit unit, int posW, int posH)
        {
            // TODO в будущем добавить проверку на то, может ли юнит дойти 
            // или стать в текущих координатах

            ((IPositionOnGrid)unit).Position = new Int2(posW, posH);

            // Синхронизировать позицию юнита на игровой сетке
            var syncData = new SyncPositionOnGridGroup(unit);
            _syncService.Add(unit.OwnerActorId, syncData);
        }
    }
}
