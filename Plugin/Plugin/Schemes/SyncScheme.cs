using Plugin.Interfaces;
using System.Collections.Generic;

namespace Plugin.Schemes
{
    /// <summary>
    /// Зберігає список дій, котрі гравець виконав на поточному кроці 
    /// </summary>
    public class SyncScheme
    {
        /// <summary>
        /// ActorID игрока, которому принадлежит синхронизация
        /// </summary>
        public int ActorId { get; }

        /// <summary>
        /// Крок синхронізації
        /// </summary>
        public int SyncStep { get; }

        /// <summary>
        /// Список дій, котрі гравець виконав на поточному кроці
        /// </summary>
        public List<ISyncGroupComponent> SyncActions;


        public SyncScheme( int actorId, int syncStep )
        {
            ActorId = actorId;
            SyncStep = syncStep;

            SyncActions = new List<ISyncGroupComponent>();
        }
    }
}
