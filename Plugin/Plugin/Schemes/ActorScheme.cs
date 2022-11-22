namespace Plugin.Schemes
{
    public class ActorScheme
    {
        public int ActorId { get; }
        public string UserId { get; }

        public bool IsConnected { get; set; }

        public ActorScheme(string userId, int actorId)
        {
            ActorId = actorId;
            UserId = userId;
            IsConnected = true;
        }
    }
}
