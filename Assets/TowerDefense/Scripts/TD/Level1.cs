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
                if (expansion.unlockAtWave == wave - 1)
                {
                    StartCoroutine(UnlockMapExpansion(expansion));
                }
            }
        }

        public IEnumerator UnlockMapExpansion(MapExpansion expansion)
        {
            foreach (GameObject obj in expansion.disableObjectsBeforeWave)
                obj.SetActive(false);

            yield return Helpers.GetWait(1);
            if(expansion.pauseGame)
                WaveManager.Instance.PauseSpawning(false);
            yield return new WaitUntil(() => GameManager.Enemies.Count == 0 && !WaveManager.Instance.isSpawningEnemies);
            yield return Helpers.GetWait(1);

            if (expansion.resetCameraPosition && CameraMovement.Instance)
            {
                CameraMovement.Instance.SetPosition(Vector3.zero, 8.4375f);
                yield return Helpers.GetWait(0.25f);
            }

            foreach (GameObject obj in expansion.objects)
                obj.SetActive(true);
            foreach (GameObject obj in expansion.disableObjectsAfterWave)
                obj.SetActive(false);
            
            if(expansion.pauseGame)
                WaveManager.Instance.ShowResumeButton();
        }

        [System.Serializable]
        public class MapExpansion
        {
            public List<GameObject> objects; //enableObjectsAfterWave
            public List<GameObject> disableObjectsAfterWave;
            public List<GameObject> disableObjectsBeforeWave;
            public int unlockAtWave;
            public bool pauseGame = true;
            public bool resetCameraPosition = false;
        }
    }
}
