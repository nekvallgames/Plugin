using Plugin.Interfaces;

namespace Plugin.Schemes
{
    public class GridScheme
    {
        /// <summary>
        /// Список с селлами из которых создана игровая сетка
        /// </summary>
        public Cell[] СellsList { get; protected set; }

        /// <summary>
        /// Владелец игровой сетки
        /// </summary>
        protected int ownerActorId;

        /// <summary>
        /// Размер игровой сетки по ширине
        /// </summary>
        protected int sizeGridW;

        /// <summary>
        /// Размер игровой сетки по высоте
        /// </summary>
        protected int sizeGridH;

        public GridScheme(int ownerActorId, int sizeGridW, int sizeGridH, Cell[] cellsList)
        {
            this.ownerActorId = ownerActorId;
            this.sizeGridW = sizeGridW;
            this.sizeGridH = sizeGridH;
            СellsList = cellsList;
        }
    }
}
