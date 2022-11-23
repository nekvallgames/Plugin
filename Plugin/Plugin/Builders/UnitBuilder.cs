using Plugin.Installers;
using Plugin.Interfaces;
using Plugin.Runtime.Services;
using Plugin.Runtime.Units;
using System;

namespace Plugin.Builders
{
    public class UnitBuilder
    {
        private UnitInstanceService _unitInstanceService;

        public UnitBuilder()
        {
            _unitInstanceService = GameInstaller.GetInstance().unitInstanceService;
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
                case UnitPistol.UnitId:
                    {
                        return new UnitPistol(ownerActorId,
                                              unitId,
                                              _unitInstanceService.GetInstance(ownerActorId, unitId));
                    }
                    break;

                case UnitShotGun.UnitId:
                    {
                        return new UnitShotGun(ownerActorId,
                                               unitId,
                                               _unitInstanceService.GetInstance(ownerActorId, unitId));
                    }
                    break;
            }

            throw new ArgumentException($"UnitBuilder :: CreateUnit() I can't create unitId = {unitId}, for actorId = {ownerActorId}.");
        }
    }
}
