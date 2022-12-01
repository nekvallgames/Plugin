using Plugin.Interfaces;

namespace Plugin.Schemes
{
    /// <summary>
    /// Схема, котра буде зберігати в данні ігрового сценарія PVP
    /// Поточна схема буде лежати в моделі PlotsPrivateModel
    /// </summary>
    public class PVPPlotModelScheme : IPlotModelScheme
    {
        /// <summary>
        /// Поточний крок синхронізації ігрового сценарія
        /// </summary>
        public int SyncStep { get; set; }

        /// <summary>
        /// Список із акторів, кому належить поточна модель
        /// </summary>
        public int OwnerActorId { get; }

        public PVPPlotModelScheme(int ownerActorId)
        {
            OwnerActorId = ownerActorId;
        }
    }
}
