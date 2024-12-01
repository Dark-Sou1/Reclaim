using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Giacomo
{
    public class DisplayInfoUI : MonoBehaviour
    {
        [SerializeField] TMP_Text title;
        [SerializeField] TMP_Text description;

        [SerializeField] GameObject statParent;
        [SerializeField] GameObject statInfoPrefab;

        [SerializeField] DisplayStatsSettings statsSettings;

        public void Show(DisplayInfo info)
        {
            title.text = info.title;
            description.text = info.description;

            statParent.transform.DestroyChildren();
        
            foreach (var stat in info.showStats.stats)
            {
                var statInfo = statsSettings.GetStatInfo(stat.Key);
                if (statInfo == null)
                    continue;

                var go = Instantiate(statInfoPrefab, statParent.transform);
                var statUI = go.GetComponent<DisplayStatUI>();
                
                statUI.Show(statInfo.name, stat.Value);
            }


        }


        [Serializable]
        public class StatInfo
        {
            public string showName;
        }
    }



    public class DisplayInfo
    {
        public string title;
        public string description;
        public Stats showStats;
    }
}