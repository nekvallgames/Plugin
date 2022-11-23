using Plugin.Tools;

namespace Plugin.Interfaces.Units
{
    public interface IPositionOnGrid
    {
        /// <summary>
        /// Позиція на ігровій сітці
        /// </summary>
        Int2 Position { get; set; }
    }
}
