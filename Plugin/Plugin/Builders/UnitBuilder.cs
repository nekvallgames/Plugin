using Plugin.Interfaces;
using Plugin.Runtime.Units;
using System;

namespace Plugin.Builders
{
    public class UnitBuilder
    {
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
                                              GetInstanceID(ownerActorID));
                    }
                    break;
            }

            throw new ArgumentException($"UnitBuilder :: CreateUnit() I can't create unitId = {unitId}, for actorId = {ownerActorId}.");
        }
    }
}
