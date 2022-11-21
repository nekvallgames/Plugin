using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Runtime.Services.Sync;
using Plugin.Runtime.Services.Sync.Groups;

namespace Plugin.Runtime.Services.ExecuteAction
{
    /// <summary>
    /// Сервіс, котрий переміщає юнітів по ігровій сітці
    /// </summary>
    public class ExecuteMoveService
    {
        private SyncService _syncService;

        public ExecuteMoveService()
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

            unit.PositionOnGridW = posW;
            unit.PositionOnGridH = posH;

            // Синхронизировать позицию юнита на игровой сетке
            var syncData = new SyncPositionOnGridGroup(unit);
            _syncService.Add(unit.OwnerActorID, syncData);
        }

        /// <summary>
        /// Переместить юнита с текущей его позиции в позицию moveToPosW, moveToPosH
        /// на игровой сетке, но с проверками, может ли юнит дойти в эти координаты
        /// </summary>
        public void MoveTo(IUnit unit, uint moveToPosW, uint moveToPosH)
        {

        }
    }
}
