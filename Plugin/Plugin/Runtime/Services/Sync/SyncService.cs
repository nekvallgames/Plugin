﻿using Plugin.Interfaces;
using Plugin.Models.Private;
using Plugin.Schemes;
using System.Linq;

namespace Plugin.Runtime.Services.Sync
{
    /// <summary>
    /// Сервіс, котрий зберігає в собі виконані дії юнітів
    /// Що би в подальшому синхронізувати дії юнітів на стороні сервера із діями на стороні клієнту
    /// </summary>
    public class SyncService
    {
        private SyncPrivateModel<SyncScheme> _syncPrivateModel;
        private PlotsPrivateModel<IPlotScheme> _plotsPrivateModel;

        public SyncService(SyncPrivateModel<SyncScheme> syncPrivateModel, PlotsPrivateModel<IPlotScheme> plotsPrivateModel)
        {
            _syncPrivateModel = syncPrivateModel;
            _plotsPrivateModel = plotsPrivateModel;

        }

        /// <summary>
        /// Зберегти дію гравця, щоб потім синхронувати цю дію між гравцями
        /// </summary>
        public void Add( int actorId, ISyncGroupComponent syncData )
        {
            int plotStep = _plotsPrivateModel.Items[0].SyncStep;    // поточний крок ігрового сценарія

            var syncStep = Get(actorId, plotStep);

            // Нужно перебрать все компоненты в syncAction,
            // и проставить глобальный шаг и обьединить все компоненты в группу
            foreach (ISyncComponent syncComponent in syncData.SyncElements)
            {
                syncComponent.SyncStep = plotStep;                   // глобальный шаг, которому принадлежит синхронизация
                syncComponent.GroupIndex = syncStep.SyncGroups.Count;  // обьединить все компоненты в группу
            }

            syncStep.SyncGroups.Add(syncData);
        }

        public SyncScheme Get(int actorId, int syncStep)
        {
            if (_syncPrivateModel.Items.Any(x => x.ActorId == actorId && x.SyncStep == syncStep)){
                return _syncPrivateModel.Items.Find(x => x.ActorId == actorId && x.SyncStep == syncStep);
            }

            return new SyncScheme(actorId, syncStep);
        }
    }
}
