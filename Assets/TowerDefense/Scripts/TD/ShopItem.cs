using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Giacomo
{
    public class ShopItem : Interactable2D
    {
        public GameObject prefab;
        public Image iconUI;
        public TMP_Text costUI;

        protected Tower tower;
        protected override void ManagedInitialize()
        {
            tower = prefab.GetComponent<Tower>();
            iconUI.sprite = tower.shopIcon;
            costUI.text = tower.cost.ToString();
            GameStats.Instance.coinsChanged += UpdatePriceColor;
            UpdatePriceColor();
        }

        public void Buy()
        {
            if (InputManager.Instance && !InputManager.Instance.acceptInput && !InputManager.Instance.placingTower)
                return;
            if (GameStats.Instance && GameStats.Instance.coins < tower.cost)
                return;

            TowerPlacementManager.Instance.StartPlacing(tower, OnTowerPlaced);
        }

        protected void OnTowerPlaced()
        {
            GameStats.Instance?.ModifyCoins(-tower.cost);
        }

        protected void UpdatePriceColor()
        {
            if(tower.cost <= GameStats.Instance.coins)
                costUI.color = TDColors.AffordableColor;
            else
                costUI.color = TDColors.UnaffordableColor;
            
        }
    }

}
