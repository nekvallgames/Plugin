namespace Plugin.Signals
{
    /// <summary>
    /// Подія, коли модель із даними ігрових сіток була змінена
    /// </summary>
    public class GridsPrivateModelSignal : ModelChangeSignal
    {
        public int OwnerActorId { get; }

        public GridsPrivateModelSignal(int ownerActorId, StatusType status) : base(status)
        {
            OwnerActorId = ownerActorId;
        }
    }
}
