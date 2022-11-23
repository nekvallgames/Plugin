namespace Plugin.Interfaces.Units
{
    public interface IUnit
    {
        /// <summary>
        /// Владелец юнита
        /// </summary>
        int OwnerActorId { get; }

        /// <summary>
        /// ID юнита
        /// </summary>
        int UnitId { get; }

        /// <summary>
        /// Получить номер инстанса юнита
        /// </summary>
        int InstanceId { get; } 
    }
}
