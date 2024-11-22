using TMPro;
using UnityEngine;

namespace Giacomo
{
    public class WaveCountdown : MonoBehaviour
    {
        TMP_Text timerText;
        private void Start()
        {
            timerText = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            if (WaveManager.Instance.NextWaveTime > Time.time 
                && !WaveManager.Instance.SpawningPaused 
                && !WaveManager.Instance.isSpawningEnemies)
            {
                float timeLeft = Mathf.Round(WaveManager.Instance.NextWaveTime - Time.time);
                timerText.text = "New wave in " + timeLeft + "s";
            }
            else
            {
                timerText.text = "";
            }
        }
    }
}