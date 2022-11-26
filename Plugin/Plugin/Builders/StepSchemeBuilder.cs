using Plugin.Interfaces;
using Plugin.Runtime.Services.Sync;
using Plugin.Schemes;
using System.Collections.Generic;

namespace Plugin.Builders
{
    /// <summary>
    /// Білдер, котрий створить StepScheme дій гравця
    /// </summary>
    public class StepSchemeBuilder
    {
        private SyncService _syncService;

        public StepSchemeBuilder( SyncService syncService )
        {
            _syncService = syncService;
        }

        /// <summary>
        /// Створити StepScheme для вказаних кроків синхронізації
        /// </summary>
        public StepScheme Create(int actorId, int[] syncSteps)
        {
            var scheme = new StepScheme();

            for (int i = 0; i < syncSteps.Length; i++)
            {
                int syncStep = syncSteps[i];

                List<ISyncGroupComponent> syncGroups = _syncService.Get(actorId, syncStep).SyncGroups;

                foreach ( ISyncComponent component in syncGroups ){
                    scheme.Add(component);
                }
            }

            return scheme;
        }
    }
}
