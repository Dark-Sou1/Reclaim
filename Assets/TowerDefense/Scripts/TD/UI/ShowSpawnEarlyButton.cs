using UnityEngine;

namespace Giacomo
{
    public class ShowSpawnEarlyButton : MonoBehaviour
    {
        //late update so it doesn't trigger in the same frame as it's starting to spawn
        private void LateUpdate()
        {
            if (!WaveManager.Instance.isSpawningEnemies && GameManager.Enemies.Count == 0)
            {
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