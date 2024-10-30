using UnityEngine;
using TMPro;

namespace Giacomo
{
    public class StatsUIManager : MonoBehaviour
    {
        public TMP_Text lives;
        public TMP_Text coins;
        //public TMP_Text waves;

        private void Awake()
        {
            GameStats.Instance.livesChanged += UpdateLives;
            GameStats.Instance.coinsChanged += UpdateCoins;
        }

        private void Start()
        {
            UpdateLives();
            UpdateCoins();
        }

        protected void UpdateLives()
        {
            lives.text = GameStats.Instance.lives.ToString();
        }
        protected void UpdateCoins()
        {
            coins.text = GameStats.Instance.coins.ToString();
        }
    }
}