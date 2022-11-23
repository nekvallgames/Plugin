using Plugin.Tools;

namespace Plugin.Schemes
{
    /// <summary>
    /// Схема, котра буде зберігати в собі дані локації, із котрої буде створена ігрова сітка
    /// </summary>
    public class LocationScheme
    {
        public string Name { get; }

        public Int2 SizeGrid { get; }

        public int[] GridMask { get; }

        public LocationScheme(string name, Int2 sizeGrid, int[] gridMask)
        {
            Name = name;
            SizeGrid = sizeGrid;
            GridMask = gridMask;
        }
    }
}
