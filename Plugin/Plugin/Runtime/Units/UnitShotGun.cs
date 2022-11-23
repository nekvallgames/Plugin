using Plugin.Interfaces.UnitComponents;
using Plugin.Tools;

namespace Plugin.Runtime.Units
{
    /// <summary>
    /// Юнит с огнестрельным дробовиком
    /// </summary>
    public class UnitShotGun : BaseDamageActionUnit, IHealthComponent, IVipComponent
    {
        public const int UnitId = 1;

        public override Int2 BodySize => new Int2(2, 5);

        int IHealthComponent.Capacity { get; set; }
        int IHealthComponent.CapacityMax => 100;    // Количество жизней юнита на старте игры

        public override int OriginalPower => 0;
        public override int OriginalCapacity => 999;

        public override Int2[] DamageActionArea => new Int2[] { new Int2(-1, 0), new Int2(0, -1), new Int2(1, 0) };

        bool IVipComponent.Enable { get; set; }



        public UnitShotGun(int ownerActorId, int unitId, int instanceUnitId) : base(ownerActorId, unitId, instanceUnitId)
        {

        }
    }
}
