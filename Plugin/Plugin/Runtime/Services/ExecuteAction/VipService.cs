using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Interfaces.UnitComponents;
using Plugin.Runtime.Services.Sync;
using Plugin.Runtime.Services.Sync.Groups;
using System;
using System.Collections.Generic;

namespace Plugin.Runtime.Services.ExecuteAction
{
    /// <summary>
    /// Сервіс, котрий буде змінювати статус VIP для юнітів
    /// Vip може бути тільки 1 юніт
    /// </summary>
    public class VipService
    {
        private SyncService _syncService;
        private UnitsService _unitsService;

        public VipService(SyncService syncService, UnitsService unitsService)
        {
            _syncService = syncService;
            _unitsService = unitsService;
        }

        public void ChangeVip( IUnit unitNextVip, bool enable )
        {
            // Перевіряємо, чи може поточний юніт бути vip?
            if (!_unitsService.HasComponent<IVipComponent>(unitNextVip)){
                throw new ArgumentException($"ExecuteVipService :: ChangeVip() actorID = {unitNextVip.OwnerActorId}, unitId = {unitNextVip.UnitId}, instanceId = {unitNextVip.InstanceId}. Unit don't has implementation IVip");
            }

            // Мертвого юніта не можемо зробити vip
            if (_unitsService.IsDead(unitNextVip)){
                throw new ArgumentException($"ExecuteVipService :: ChangeVip() actorID = {unitNextVip.OwnerActorId}, unitId = {unitNextVip.UnitId}, instanceId = {unitNextVip.InstanceId}. Unit alredy dead. I can't make it vip");
            }

            // Деактивуємо vip для всих юнітів актора
            List<IUnit> units = _unitsService.GetUnits(unitNextVip.OwnerActorId);
            foreach (IUnit unit in units)
            {
                if (_unitsService.HasComponent<IVipComponent>(unit)){
                    ((IVipComponent)unit).Enable = false;
                }
            }

            // Активуємо vip для поточного юніта 
            ((IVipComponent)unitNextVip).Enable = enable;

            // Синхронизировать статус Vip для юнита
            var syncVip = new SyncVipGroup(unitNextVip);
            _syncService.Add(unitNextVip.OwnerActorId, syncVip);
        }
    }
}
