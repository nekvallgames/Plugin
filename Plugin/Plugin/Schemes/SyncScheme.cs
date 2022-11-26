using Plugin.Interfaces;
using System.Collections.Generic;

namespace Plugin.Schemes
{
    /// <summary>
    /// Зберігає список дій, котрі гравець виконав на поточному кроці 
    /// </summary>
    public struct SyncScheme
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
        public List<ISyncGroupComponent> SyncGroups;


        public SyncScheme( int actorId, int syncStep )
        {
            ActorId = actorId;
            SyncStep = syncStep;

            SyncGroups = new List<ISyncGroupComponent>();
        }
    }
}
