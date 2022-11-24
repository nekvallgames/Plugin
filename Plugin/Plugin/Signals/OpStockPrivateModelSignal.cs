using Plugin.Interfaces;

namespace Plugin.Signals
{
    /// <summary>
    /// Подія, коли модель із даними операцій акторів була змінена
    /// </summary>
    public struct OpStockPrivateModelSignal : ISignal
    {
        /// <summary>
        /// Власник операції
        /// </summary>
        public int ActorId { get; }
        /// <summary>
        /// Код операції знаходиться в классі OperationCode
        /// </summary>
        public byte OpCode { get; }

        /// <summary>
        /// Статус змінення моделі
        /// add - була добавлена нова операція
        /// remove - операція була оброблена і видалена
        /// </summary>
        public StatusType Status { get; }

        public enum StatusType
        {
            add,
            remove
        }

        public OpStockPrivateModelSignal(int actorId, byte opCode, StatusType status)
        {
            ActorId = actorId;
            OpCode = opCode;
            Status = status;
        }
    }
}
