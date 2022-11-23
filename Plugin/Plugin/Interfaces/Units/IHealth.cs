namespace Plugin.Interfaces.Units
{
    public interface IHealth
    {
        /// <summary>
        /// Получить количество жизней юнита
        /// </summary>
        int Capacity { get; set; }

        /// <summary>
        /// Получить максимальное количество жизней юнита
        /// </summary>
        int CapacityMax { get; }
    }
}
