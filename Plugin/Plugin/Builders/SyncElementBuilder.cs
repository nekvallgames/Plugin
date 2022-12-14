using Plugin.Interfaces;
using Plugin.OpComponents;
using Plugin.Tools;

namespace Plugin.Builders
{
    /// <summary>
    /// Билдер, для созранения елементов синхронизации SERVER - CLIENT
    /// </summary>
    public class SyncElementBuilder
    {
        public ISyncGroupComponent Sync { get; private set; }

        public static SyncElementBuilder Build(ISyncGroupComponent sync)
        {
            return new SyncElementBuilder { Sync = sync };
        }

        private SyncElementBuilder()
        {

        }

        /// <summary>
        /// Синхронизировать уникальный идентификатор юнита
        /// </summary>
        public SyncElementBuilder SyncUnitID(int unitID, int instanceID)
        {
            var syncElementUnitId = new UnitIdOpComponent()
            {
                uid = unitID,
                i = instanceID
            };

            Sync.SyncElements.Add(syncElementUnitId);
            return this;
        }

        /// <summary>
        /// Синхронизировать позицию на игровой сетке
        /// </summary>
        public SyncElementBuilder SyncPositionOnGrid(Int2 position)
        {
            var syncElementPositionOnGrid = new PositionOnGridOpComponent
            {
                w = position.x,
                h = position.y
            };

            Sync.SyncElements.Add(syncElementPositionOnGrid);
            return this;
        }

        /// <summary>
        /// Синхронизировать точку, в которою игрок выполнил свой экшен
        /// </summary>
        public SyncElementBuilder SyncAction(int cellW, int cellH)
        {
            var syncElementAction = new ActionOpComponent
            {
                w = cellW,
                h = cellH
            };

            Sync.SyncElements.Add(syncElementAction);
            return this;
        }

        /// <summary>
        /// Синхронизировать точку, в которою игрок выполнил свой дополнительный (пассивный) экшен
        /// </summary>
        public SyncElementBuilder SyncAdditional(int cellW, int cellH)
        {
            var syncElementAdditional = new AdditionalOpComponent
            {
                w = cellW,
                h = cellH
            };

            Sync.SyncElements.Add(syncElementAdditional);
            return this;
        }

        /// <summary>
        /// Синхронизировать ID актера, к которому скорее всего
        /// было выполнено текущее действие игрока
        /// </summary>
        public SyncElementBuilder SyncTargetActorID(int actorID)
        {
            var saveElementTargetActorID = new TargetActorIdOpComponent()
            {
                aid = actorID
            };

            Sync.SyncElements.Add(saveElementTargetActorID);
            return this;
        }

        /// <summary>
        /// Синхронизировать Vip статус юнита
        /// </summary>
        public SyncElementBuilder SyncVip(bool enable)
        {
            var syncElementVip = new VipOpComponent()
            {
                e = enable
            };

            Sync.SyncElements.Add(syncElementVip);
            return this;
        }

    }
}
