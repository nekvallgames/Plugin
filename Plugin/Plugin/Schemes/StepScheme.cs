using Plugin.Interfaces;
using Plugin.OpComponents;
using System;
using System.Collections.Generic;

namespace Plugin.Schemes
{
    /// <summary>
    /// Схема "Игрок сделал 2-а игровых шага"
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
    }
}
