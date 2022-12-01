using System.Collections.Generic;

namespace Plugin.Interfaces
{
    public interface IPlotModelScheme
    {
        /// <summary>
        /// Список із акторів, кому належить поточна модель
        /// </summary>
        int OwnerActorId { get; }

        /// <summary>
        /// Поточний крок синхронізації ігрового сценарія
        /// </summary>
        int SyncStep { get; set; }
    }
}
