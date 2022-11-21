using Plugin.Interfaces;

namespace Plugin.Schemes
{
    public struct OpScheme
    {
        public int ActorId;

        /// <summary>
        /// Код операции
        /// </summary>
        public byte OpCode { get; }

        /// <summary>
        /// Данные операции
        /// </summary>
        public object Data { get; }

        public OpScheme( int actorId, byte evCode, object data )
        {
            ActorId = actorId;
            OpCode = evCode;
            Data = data;
        }
    }
}
