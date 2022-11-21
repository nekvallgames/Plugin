using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Runtime.Services.Sync;
using Plugin.Runtime.Services.Sync.Groups;

namespace Plugin.Runtime.Services.ExecuteAction
{
    public class ExecuteVipService
    {
        private SyncService _syncService;

        public ExecuteVipService()
        {
            _syncService = GameInstaller.GetInstance().syncService;
        }

        public void ChangeVip(IUnit unit, bool isVip)
        {
            // TODO в будущем добавить проверку на то, может ли юнит вообще быть VIP
            ((IVip)unit).IsVip = isVip;

            // Синхронизировать статус Vip для юнита
            var syncVip = new SyncVipGroup(unit, isVip);
            _syncService.Add(unit.OwnerActorID, syncVip);
        }
    }
}
