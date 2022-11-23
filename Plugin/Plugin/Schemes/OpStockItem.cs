using Plugin.Interfaces;

namespace Plugin.Schemes
{
    public struct OpStockItem : IOpStockItem
    {
        public int ActorId { get; }

        /// <summary>
        /// Код операции
        /// </summary>
        public byte OpCode { get; }

        /// <summary>
        /// Данные операции
        /// </summary>
        public object Data { get; }

        public OpStockItem( int actorId, byte evCode, object data )
        {
            ActorId = actorId;
            OpCode = evCode;
            Data = data;
        }
    }
}
