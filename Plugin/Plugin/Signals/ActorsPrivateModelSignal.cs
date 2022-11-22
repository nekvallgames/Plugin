using Plugin.Interfaces;

namespace Plugin.Signals
{
    /// <summary>
    /// Подія, коли модель із данними гравців була оновлена
    /// </summary>
    public struct ActorsPrivateModelSignal : ISignal
    {
        public int ActorId { get; }

        public ActorsPrivateModelSignal(int actorId)
        {
            ActorId = actorId;
        }
    }
}
