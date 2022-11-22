using Plugin.Interfaces;

namespace Plugin.Schemes
{
    public struct GridScheme
    {
        /// <summary>
        /// Список с селлами из которых создана игровая сетка
        /// </summary>
        public Cell[] СellsList { get; }

        /// <summary>
        /// Владелец игровой сетки
        /// </summary>
        public int ownerActorId { get; }

        /// <summary>
        /// Размер игровой сетки по ширине
        /// </summary>
        public int sizeGridW { get; }

        /// <summary>
        /// Размер игровой сетки по высоте
        /// </summary>
        public int sizeGridH { get; }


        public GridScheme(int ownerActorId, int sizeGridW, int sizeGridH, Cell[] cellsList)
        {
            this.ownerActorId = ownerActorId;
            this.sizeGridW = sizeGridW;
            this.sizeGridH = sizeGridH;
            СellsList = cellsList;
        }
    }
}
