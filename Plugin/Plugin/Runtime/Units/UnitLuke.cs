using Plugin.Interfaces;
using Plugin.Interfaces.UnitComponents;
using Plugin.Tools;

namespace Plugin.Runtime.Units
{
    /// <summary>
    /// Обычный юнит барриер. Металевий люк
    /// </summary>
    public class UnitLuke : BaseUnit, IHealthComponent, IUnit
    {
        public const int UnitId = 11;

        public override Int2 BodySize => new Int2(3, 3);

        int IHealthComponent.Capacity { get; set; }
        int IHealthComponent.CapacityMax => 100;    // Количество жизней юнита на старте игры

        public UnitLuke(int ownerActorId, int unitId, int instanceUnitId) : base(ownerActorId, unitId, instanceUnitId)
        {

        }
    }
}

