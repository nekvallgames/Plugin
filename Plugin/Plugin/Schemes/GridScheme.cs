using Plugin.Interfaces;

namespace Plugin.Schemes
{
    public struct GridScheme : IGrid
    {
        /// <summary>
        /// Список с селлами из которых создана игровая сетка
        /// </summary>
        public Cell[] СellsList { get; }

        /// <summary>
        /// Владелец игровой сетки
        /// </summary>
        public int OwnerActorId { get; }

        /// <summary>
        /// Размер игровой сетки по ширине
        /// </summary>
        public int SizeGridW { get; }

        /// <summary>
        /// Размер игровой сетки по высоте
        /// </summary>
        public int SizeGridH { get; }


        public GridScheme(int ownerActorId, int sizeGridW, int sizeGridH, Cell[] cellsList)
        {
            OwnerActorId = ownerActorId;
            SizeGridW = sizeGridW;
            SizeGridH = sizeGridH;
            СellsList = cellsList;
        }
    }
}
