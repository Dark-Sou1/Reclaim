using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class WaveManager : Singleton<WaveManager>
    {
        public float defaultWaveDelay;
        [SerializeField] GameObject resumeSpawningButton;
        [SerializeField] protected bool spawningPaused;

        public int CurrentWave => currentWave;
        protected int currentWave;
        protected Spawner[] spawners;
    
        public event Action<int> SpawningNewWave;

        private void Start()
        {
            spawners = FindObjectsByType<Spawner>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            StartGame();
        }

        protected bool gameStarted;
        public void StartGame()
        {
            if (gameStarted) return;
            gameStarted = true;

            StartCoroutine(SpawnWaves());
        }

        public void PauseSpawning(bool showResumeButton)
        {
            spawningPaused = true;
            if(showResumeButton)
                resumeSpawningButton?.SetActive(true);
        }

        public void ShowResumeButton()
        {
            resumeSpawningButton?.SetActive(true);
        }

        public void ResumeSpawning()
        {
            spawningPaused = false;
            resumeSpawningButton?.SetActive(false);
            
        }

        protected IEnumerator SpawnWaves()
        {
            while (!CheckGameOver())
            {
                if (spawningPaused)
                    yield return new WaitUntil(() => !spawningPaused);

                SpawningNewWave?.Invoke(currentWave);
                float maxDelay = -1;
                foreach (Spawner spawner in spawners)
                {
                    float waveDelay = spawner.SpawnNextWave();
                    maxDelay = Mathf.Max(maxDelay, waveDelay);
                }

                if (maxDelay <= 0)
                    yield return Helpers.GetWait(defaultWaveDelay);
                else
                    yield return Helpers.GetWait(maxDelay);
            
                currentWave++;
            }
        }

        protected bool CheckGameOver()
        {
            foreach(Spawner spawner in spawners)
                if (!spawner.finishedWaves)
                    return false;

            return true;
        }

    }

}