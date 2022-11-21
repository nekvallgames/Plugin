using Plugin.Builders;
using Plugin.Interfaces;
using System.Collections.Generic;

namespace Plugin.Runtime.Services.Sync.Groups
{
    /// <summary>
    /// Игрок на стороне сервера применил дополнительное (пассивное) действие юнита
    /// Создать группу из компонентов, которые нужны, что бы синхронизировать выполненное действие между клиентами
    /// </summary>
    public class SyncAdditionalGroup : ISyncGroupComponent
    {
        public List<ISyncComponent> SyncElements { get; }

        // Constructor
        public SyncAdditionalGroup(IUnit unit, int targetActorID, int positionOnGridW, int positionOnGridH)
        {
            SyncElements = new List<ISyncComponent>();

            var syncElements = SyncElementBuilder
               .Build(this)
               .SyncUnitID(unit.UnitID, unit.InstanceID)
               .SyncAdditional(positionOnGridW, positionOnGridH)
               .SyncTargetActorID(targetActorID);
        }


    }
}
