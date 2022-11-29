namespace Plugin.Signals
{
    /// <summary>
    /// Подія, коли модель із данними гравців була оновлена
    /// </summary>
    public class ActorsPrivateModelSignal : ModelChangeSignal
    {
        public int ActorId { get; }

        public ActorsPrivateModelSignal(int actorId, StatusType status) : base(status)
        {
            ActorId = actorId;
        }
    }
}
