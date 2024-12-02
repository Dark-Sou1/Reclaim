using TMPro;
using Unity.VisualScripting;
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
            //If already placing, deselect instead
            if(TowerPlacementManager.Instance.placingTower == tower)
            {
                TowerPlacementManager.Instance.StopPlacing();
                return;
            }
            //Check input is not occupied (for example playing a potion or a different tower)
            if (InputManager.Instance && !InputManager.Instance.acceptInput)
                return;
            //Check player has enough money
            if (GameStats.Instance && GameStats.Instance.coins < tower.cost)
                return;

            TowerPlacementManager.Instance.StartPlacing(tower, OnTowerPlaced);
        }

        public void OnCursorHover()
        {
            DisplayInfoUI.Instance.Show(this, tower.shopIcon, tower.towerName, tower.towerDescription);
        }
        public void OnCursorExit()
        {
            DisplayInfoUI.Instance.Hide(this);
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
