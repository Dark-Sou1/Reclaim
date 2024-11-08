using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Giacomo
{
    public class Level1 : MonoBehaviour
    {
        public List<MapExpansion> expansions;



        private void Awake()
        {
            WaveManager.Instance.SpawningNewWave += OnStartedWave;
        }

        protected void OnStartedWave(int wave)
        {
            foreach(MapExpansion expansion in expansions)
            {
                if (expansion.unlockAtWave == wave)
                {
                    StartCoroutine(UnlockMapAfterWave(expansion));
                }
            }
        }

        public IEnumerator UnlockMapAfterWave(MapExpansion expansion)
        {
            yield return Helpers.GetWait(1);
            WaveManager.Instance.PauseSpawning(false);
            yield return new WaitUntil(() => GameManager.Enemies.Count == 0);
            yield return Helpers.GetWait(1);

            foreach (GameObject obj in expansion.objects)
                obj.SetActive(true);
            
            WaveManager.Instance.ShowResumeButton();
        }

        [System.Serializable]
        public class MapExpansion
        {
            public List<GameObject> objects;
            public int unlockAtWave;
        }
    }

}