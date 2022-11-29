namespace Plugin.Interfaces
{
    public interface IPlotModelScheme
    {
        /// <summary>
        /// Поточний крок синхронізації ігрового сценарія
        /// </summary>
        int SyncStep { get; set; }
    }
}
