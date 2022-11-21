namespace Plugin.Interfaces
{
    public interface IUnit
    {
        /// <summary>
        /// Владелец юнита
        /// </summary>
        int OwnerActorID { get; }

        /// <summary>
        /// ID юнита
        /// </summary>
        int UnitID { get; }

        /// <summary>
        /// Получить номер инстанса юнита
        /// </summary>
        int InstanceID { get; }

        /// <summary>
        /// Получить количество жизней юнита
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Получить максимальное количество жизней юнита
        /// </summary>
        int GetMaxHealth { get; }

        /// <summary>
        /// Получить количество брони юнита
        /// </summary>
        int Armor { get; set; }

        /// <summary>
        /// Ширина юнита на игровой сетке
        /// </summary>
        int AreaWidth { get; }

        /// <summary>
        /// Высота юнита на игровой сетке
        /// </summary>
        int AreaHeight { get; }

        /// <summary>
        /// Позиция на игровой сетке по ширине
        /// </summary>
        int PositionOnGridW { get; set; }

        /// <summary>
        /// Позиция на игровой сетке по высоте
        /// </summary>
        int PositionOnGridH { get; set; }

        /// <summary>
        /// Восстановить экшены юнита
        /// </summary>
        void ReviveAction();
    }
}
