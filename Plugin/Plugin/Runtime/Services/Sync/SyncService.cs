using Plugin.Interfaces;
using Plugin.Models.Private;
using Plugin.Schemes;

namespace Plugin.Runtime.Services.Sync
{
    public class SyncService
    {
        private SyncPrivateModel<SyncScheme> _model;

        /// <summary>
        /// Глобальний крок синхронізації
        /// </summary>
        public int SyncStep { get; private set; }

        public SyncService(SyncPrivateModel<SyncScheme> model)
        {
            _model = model;
        }

        public void IncreaseSyncStep()
        {
            SyncStep++;
        }

        /// <summary>
        /// Зберегти дію гравця, щоб потім синхронувати цю дію між гравцями
        /// </summary>
        public void Add( int actorId, ISyncGroupComponent syncData )
        {
            var actorSync = _model.Items.Find(x => x.ActorId == actorId && x.SyncStep == SyncStep);

            // Нужно перебрать все компоненты в syncAction,
            // и проставить глобальный шаг и обьединить все компоненты в группу
            foreach (ISyncComponent syncComponent in syncData.SyncElements)
            {
                syncComponent.HistoryStep = SyncStep;                // глобальный шаг, которому принадлежит синхронизация
                syncComponent.GroupIndex = actorSync.SyncActions.Count;    // обьединить все компоненты в группу
            }

            actorSync.SyncActions.Add(syncData);
        }
    }
}
