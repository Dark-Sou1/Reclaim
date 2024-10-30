using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giacomo
{
    public class GameStats : Singleton<GameStats>
    {
        public int lives;
        public int coins;
        public event Action livesChanged;
        public event Action coinsChanged;

        public void LoseHP(int amount)
        {
            lives -= amount;
            if (lives <= 0)
                GameManager.Instance.LoseGame();
            livesChanged?.Invoke();
        }


        public void ModifyCoins(int amount)
        {
            coins += amount;
            coinsChanged?.Invoke();
        }
    }
}