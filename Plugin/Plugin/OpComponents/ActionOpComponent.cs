using Plugin.Interfaces;
using System;

namespace Plugin.OpComponents
{
    [Serializable]
    public struct ActionOpComponent : ISyncComponent
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
        // positionW
        public int w;
        // positionH
        public int h;
    }
}
