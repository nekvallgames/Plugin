using Plugin.Interfaces;
using Plugin.OpComponents;
using System;
using System.Collections.Generic;

namespace Plugin.Schemes
{
    /// <summary>
    /// Схема ігрового кроку
    /// </summary>
    [Serializable]
    public class StepScheme : IOpScheme
    {
        public List<ActionOpComponent> syncActions;
        public List<AdditionalOpComponent> syncAdditional;
        public List<PositionOnGridOpComponent> syncPositionOnGrid;
        public List<TargetActorIdOpComponent> syncTargetActorID;
        public List<UnitIdOpComponent> syncUnitID;
        public List<VipOpComponent> syncVip;
        

        public StepScheme()
        {
            syncActions = new List<ActionOpComponent>();
            syncAdditional = new List<AdditionalOpComponent>();
            syncPositionOnGrid = new List<PositionOnGridOpComponent>();
            syncUnitID = new List<UnitIdOpComponent>();
            syncTargetActorID = new List<TargetActorIdOpComponent>();
            syncVip = new List<VipOpComponent>();
        }
        
        public void Add( ISyncComponent component )
        {
            // TODO поки що не можу реалізувати по інакшому, не вистачає досвіду

            if (component.GetType() == typeof(ActionOpComponent))
                syncActions.Add((ActionOpComponent)component);

            if (component.GetType() == typeof(AdditionalOpComponent))
                syncAdditional.Add((AdditionalOpComponent)component);

            if (component.GetType() == typeof(PositionOnGridOpComponent))
                syncPositionOnGrid.Add((PositionOnGridOpComponent)component);

            if (component.GetType() == typeof(UnitIdOpComponent))
                syncUnitID.Add((UnitIdOpComponent)component);

            if (component.GetType() == typeof(TargetActorIdOpComponent))
                syncTargetActorID.Add((TargetActorIdOpComponent)component);

            if (component.GetType() == typeof(VipOpComponent))
                syncVip.Add((VipOpComponent)component);
        }

    }
}
