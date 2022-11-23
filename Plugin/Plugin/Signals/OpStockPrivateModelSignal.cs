﻿using Plugin.Interfaces;

namespace Plugin.Signals
{
    /// <summary>
    /// Событие, игрок прислал на GAME SERVER операцию 
    /// и положили ее на слад операций игрока
    /// </summary>
    public struct OpStockPrivateModelSignal : ISignal
    {
        public int ActorId { get; }
        public byte OpCode { get; }

        public OpStockPrivateModelSignal(int actorId, byte opCode)
        {
            ActorId = actorId;
            OpCode = opCode;
        }
    }
}
