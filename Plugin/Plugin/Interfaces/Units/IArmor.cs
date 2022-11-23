namespace Plugin.Interfaces.Units
{
    public interface IArmor
    {
        /// <summary>
        /// Получить количество брони юнита
        /// </summary>
        int Capacity { get; set; }
        
        /// <summary>
        /// Получить максимальное количество брони юнита
        /// </summary>
        int CapacityMax { get; set; }
    }
}
