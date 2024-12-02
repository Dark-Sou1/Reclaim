using UnityEngine;

namespace Giacomo
{
    public class ShowSpawnEarlyButton : MonoBehaviour
    {
        bool activating = false;
        //late update so it doesn't trigger in the same frame as it's starting to spawn
        private void LateUpdate()
        {
            if (!activating 
                && !WaveManager.Instance.isSpawningEnemies 
                && GameManager.Enemies.Count == 0
                && WaveManager.Instance.NextWaveTime - Time.time > 0.5f)
            {
                activating = true;
                int wave = WaveManager.Instance.CurrentWave;
                CustomCoroutine.WaitThenExecute(1f,
                    () => {
                        if (!WaveManager.Instance.isSpawningEnemies)
                            WaveManager.Instance.ShowResumeButton();
                    });
            }
        }
    }
}