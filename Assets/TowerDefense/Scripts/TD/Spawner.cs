using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace Giacomo
{
    public class Spawner : MonoBehaviour
    {
        public List<Wave> waves;
        public float defaultWaveDelay;



        [DisableInEditorMode] public int currentWaveIndex;
        public Wave currentWave => waves[currentWaveIndex];

        private void Start()
        {
            currentWaveIndex = 0;
            StartSpawning();
        }

        public void StartSpawning()
        {
            StartCoroutine(SpawnWaves());
        }

        protected IEnumerator SpawnWaves()
        {
            while (currentWaveIndex < waves.Count)
            {
                for (int i = 0; i < currentWave.amount; i++)
                {
                    foreach (Wave.WaveEnemy e in currentWave.enemies)
                    {
                        Instantiate(e.prefab, transform.position, Quaternion.identity);
                        yield return Helpers.GetWait(e.delay);
                    }
                }

                float delay = defaultWaveDelay;
                if (currentWave.advancedSettings.customDelayAfterWave != -1)
                    delay = currentWave.advancedSettings.customDelayAfterWave;
                yield return Helpers.GetWait(delay);

                currentWaveIndex++;
            }
        }
    }





    [Serializable]
    public class Wave
    {
        public AdvancedSettings advancedSettings;
        [TableList(ShowIndexLabels = true)]
        public List<WaveEnemy> enemies;
        public int amount = 5;

        [Serializable]
        public class WaveEnemy
        {
            [TableColumnWidth(50, Resizable = false)]
            [PreviewField(Alignment = ObjectFieldAlignment.Center)]
            public GameObject prefab;
            [VerticalGroup("Settings"), LabelWidth(50)]
            public float delay = .5f;
        }

        [Serializable]
        public class AdvancedSettings
        {
            public List<int> spawnOrder;
            public float customDelayAfterWave = -1;
        }
    }
}