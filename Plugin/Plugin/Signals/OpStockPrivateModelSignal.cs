namespace Plugin.Signals
{
    /// <summary>
    /// Подія, коли модель із даними операцій акторів була змінена
    /// </summary>
    public class OpStockPrivateModelSignal : ModelChangeSignal
    {
        /// <summary>
        /// Власник операції
        /// </summary>
        public int ActorId { get; }
        /// <summary>
        /// Код операції знаходиться в классі OperationCode
        /// </summary>
        public byte OpCode { get; }

        public OpStockPrivateModelSignal(int actorId, byte opCode, StatusType status):base(status)
        {
            ActorId = actorId;
            OpCode = opCode;
        }
    }
}
