﻿using Newtonsoft.Json;
using Plugin.Interfaces;
using System;

namespace Plugin.OpComponents
{
    [Serializable]
    public struct ActionOpComponent : ISyncComponent
    {
        [JsonIgnore]
        public int SyncStep
        {
            get { return ss; }
            set { ss = value; }
        }
        [JsonIgnore]
        public int GroupIndex
        {
            get { return gi; }
            set { gi = value; }
        }


        // syncStep
        public int ss;
        // groupIndex
        public int gi;
        // positionW
        public int w;
        // positionH
        public int h;
    }
}
