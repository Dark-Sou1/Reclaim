using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class Spawner : MonoBehaviour
    {
        public int startSpawningAfterWaves = 0;
        public List<Wave> waves;

        public bool disableAtLastWave;

        public bool isSpawning;
        public bool finishedWaves;

        protected int wavesWhileActive;
        protected int currentWaveIndex;
        public float SpawnNextWave()
        {
            if (!isActiveAndEnabled)
                return -1;

            wavesWhileActive++;
            if (wavesWhileActive < startSpawningAfterWaves)
                return -1;

            if (currentWaveIndex >= waves.Count)
                return -1;

            StartCoroutine(SpawnWaveCoroutine());
            float delayAfter = waves[currentWaveIndex].advancedSettings.customDelayAfterWave;

            currentWaveIndex++;
            if(currentWaveIndex == waves.Count)
            {
                finishedWaves = true;
                if(disableAtLastWave)
                    gameObject.SetActive(false);
            }

            return delayAfter;
        }

        protected IEnumerator SpawnWaveCoroutine()
        {
            isSpawning = true;
            Wave wave = waves[currentWaveIndex];
            for (int i = 0; i < wave.amount; i++)
            {
                foreach (Wave.WaveEnemy e in wave.enemies)
                {
                    var enemy = Instantiate(e.prefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
                    if (wave.advancedSettings.hpMultiplier != 1)
                    {
                        enemy.Initialize();
                        enemy.stats.AddModifier("baseHpModifier", "maxHealth", multiply: wave.advancedSettings.hpMultiplier);
                    }

                    yield return Helpers.GetWait(e.delay);
                }
            }
            isSpawning = false;
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
            public float customDelayAfterWave = -1;
            public float hpMultiplier = 1;
        }
    }
}