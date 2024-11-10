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
                    StartCoroutine(UnlockMapExpansion(expansion));
                }
            }
        }

        public IEnumerator UnlockMapExpansion(MapExpansion expansion)
        {
            yield return Helpers.GetWait(1);
            if(expansion.pauseGame)
                WaveManager.Instance.PauseSpawning(false);
            yield return new WaitUntil(() => GameManager.Enemies.Count == 0);
            yield return Helpers.GetWait(1);

            foreach (GameObject obj in expansion.objects)
                obj.SetActive(true);
            foreach (GameObject obj in expansion.disableObjects)
                obj.SetActive(false);
            
            if(expansion.pauseGame)
                WaveManager.Instance.ShowResumeButton();
        }

        [System.Serializable]
        public class MapExpansion
        {
            public List<GameObject> objects; //enableObjects
            public List<GameObject> disableObjects;
            public int unlockAtWave;
            public bool pauseGame = true;
        }
    }
}
