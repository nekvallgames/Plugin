using Plugin.Interfaces;
using System;

namespace Plugin.OpComponents
{
    [Serializable]
    public struct UnitIdOpComponent : ISyncComponent
    {
        public int HistoryStep
        {
            get { return hs; }
            set { hs = value; }
        }
        public int GroupIndex
        {
            get { return gi; }
            set { gi = value; }
        }

        // historyStep
        public int hs;
        // groupIndex
        public int gi;
        // unitID
        public int uid;
        // instanceID
        public int i;
    }
}
