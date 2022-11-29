using Plugin.Interfaces;
using Plugin.Interfaces.UnitComponents;
using Plugin.Tools;

namespace Plugin.Runtime.Units
{
    /// <summary>
    /// Обычный юнит барриер. Контейнер із сміттям
    /// </summary>
    public class UnitTrash : BaseUnit, IHealthComponent, IUnit
    {
        public const int UnitId = 7;

        public override Int2 BodySize => new Int2(4, 3);

        int IHealthComponent.Capacity { get; set; }
        int IHealthComponent.CapacityMax => 100;    // Количество жизней юнита на старте игры

        public UnitTrash(int ownerActorId, int unitId, int instanceUnitId) : base(ownerActorId, unitId, instanceUnitId)
        {
            
        }
    }
}
