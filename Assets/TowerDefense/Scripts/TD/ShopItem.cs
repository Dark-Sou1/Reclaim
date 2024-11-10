using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Giacomo
{
    public class ShopItem : MonoBehaviour
    {
        public GameObject prefab;
        public Image iconUI;
        public TMP_Text costUI;

        protected Tower tower;
        protected void Awake()
        {
            tower = prefab.GetComponent<Tower>();
            iconUI.sprite = tower.shopIcon;
            costUI.text = tower.cost.ToString();
            GameStats.Instance.coinsChanged += UpdatePriceColor;
            UpdatePriceColor();
        }

        public void Select()
        {
            if(TowerPlacementManager.Instance.placingTower == tower)
            {
                TowerPlacementManager.Instance.StopPlacing();
                return;
            }
            if (InputManager.Instance && !InputManager.Instance.acceptInput)
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
