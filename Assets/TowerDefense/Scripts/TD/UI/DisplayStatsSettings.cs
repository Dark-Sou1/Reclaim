using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    [CreateAssetMenu(fileName = "StatsSettings", menuName = "stats/settings")]
    public class DisplayStatsSettings : SerializedScriptableObject
    {
        [OdinSerialize, ShowInInspector] Dictionary<string, StatInfo> stats;

        public StatInfo GetStatInfo(string stat)
        {
            if (!stats.ContainsKey(stat))
                return null;
            return stats[stat];
        }

        [Serializable]
        public class StatInfo
        {
            public string name;
        }
    }
}
