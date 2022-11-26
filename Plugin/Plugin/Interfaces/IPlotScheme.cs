namespace Plugin.Interfaces
{
    public interface IPlotScheme
    {
        /// <summary>
        /// Поточний крок синхронізації ігрового сценарія
        /// </summary>
        int SyncStep { get; set; }
    }
}
