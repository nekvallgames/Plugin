using Plugin.Interfaces;
using Plugin.Runtime.Services;
using Plugin.Runtime.Units;
using System;
using System.Diagnostics;

namespace Plugin.Builders
{
    public class UnitBuilder
    {
        private UnitInstanceService _unitInstanceService;

        public UnitBuilder(UnitInstanceService unitInstanceService)
        {
            _unitInstanceService = unitInstanceService;
        }

        /// <summary>
        /// Создать юнит, указав его тип
        /// ownerActorId - владелец юнита
        /// unitId       - уникальный ID юнита
        /// </summary>
        public IUnit CreateUnit(int ownerActorId, int unitId)
        {
            switch (unitId)
            {
                case UnitPistol.UnitId: return Create<UnitPistol>(ownerActorId, unitId);
                case UnitShotGun.UnitId: return Create<UnitShotGun>(ownerActorId, unitId);
                case UnitTrash.UnitId: return Create<UnitTrash>(ownerActorId, unitId);
                case UnitRoadBlock.UnitId: return Create<UnitRoadBlock>(ownerActorId, unitId);
                case UnitBarrel.UnitId: return Create<UnitBarrel>(ownerActorId, unitId);
                case UnitLuke.UnitId: return Create<UnitLuke>(ownerActorId, unitId);
                case UnitBagBarrier.UnitId: return Create<UnitBagBarrier>(ownerActorId, unitId);
                case UnitIronFenceBarrier.UnitId: return Create<UnitIronFenceBarrier>(ownerActorId, unitId);

                default:{
                        Debug.Fail($"UnitBuilder :: CreateUnit() I can't create unitId = {unitId}, for actorId = {ownerActorId}.");
                        return null;
                    }
                    break;
            }
        }

        private IUnit Create<T>(int actorId, int unitId) where T : IUnit
        {
            int instance = _unitInstanceService.GetInstance(actorId, unitId);

            return (T)Activator.CreateInstance(typeof(T), actorId, unitId, instance);
        }
    }
}
