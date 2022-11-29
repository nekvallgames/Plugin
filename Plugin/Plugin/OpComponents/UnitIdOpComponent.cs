﻿using Newtonsoft.Json;
using Plugin.Interfaces;
using System;

namespace Plugin.OpComponents
{
    [Serializable]
    public struct UnitIdOpComponent : ISyncComponent
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
        // unitID
        public int uid;
        // instanceID
        public int i;
    }
}
