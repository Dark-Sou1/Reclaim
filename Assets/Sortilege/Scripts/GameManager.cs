using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sortilege.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GameObject gameOverPanel;

        [SerializeField] Image[] healthBars;
        public int currentHealth;

        public void ReduceHealth()
        {
            print(currentHealth);
            print(healthBars.Length);
            if (currentHealth > 0)
            {
                healthBars[currentHealth - 1].enabled = false;
                currentHealth--;
            }

            if (currentHealth <= 0)
            {
                gameOverPanel.SetActive(true);
            }
        }

        public void ContinueGame()
        {
            SceneManager.LoadScene(0);
        }
        public void EndGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
