using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Giacomo
{

    public class TowerPlacementManager : Singleton<TowerPlacementManager>
    {
        public SpriteRenderer previewRenderer;

        [ShowInInspector, ReadOnly] protected Tower placingTower;
        protected Action OnPlace;


        public void Update()
        {
            if(placingTower == null)
                return;

            if (Input.GetKey(KeyCode.Escape))
            { 
                StopPlacing();
                return;
            }

            var mousePos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
            Tile hoveringTile = GridManager.Instance.Get(mousePos);

            if (hoveringTile == null)
            {
                previewRenderer.enabled = false;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (hoveringTile != null && hoveringTile.CanPlace())
                {
                    InputManager.Instance.SetPlacingStatus(false);
                    var t = Instantiate(placingTower.gameObject).GetComponent<Tower>();
                    t.name = placingTower.name;
                    hoveringTile.PlaceTower(t);
                    OnPlace?.Invoke();

                    StopPlacing();
                    return;
                }
            }

            if (hoveringTile)
            {
                previewRenderer.transform.position = hoveringTile.transform.position;
                previewRenderer.enabled = true;

                if (hoveringTile.CanPlace())
                    previewRenderer.color = Color.white;
                else
                    previewRenderer.color = Color.red;
            }
        }

        public void StopPlacing()
        {
            placingTower = null;
            OnPlace = null;
            previewRenderer.enabled = false;
            InputManager.Instance.SetPlacingStatus(false);
        }

        public void StartPlacing(Tower tower, Action onPlace = null)
        {
            if (placingTower == tower)
            {
                StopPlacing();
                return;
            }

            InputManager.Instance.SetPlacingStatus(true);
            placingTower = tower;
            previewRenderer.sprite = placingTower.placingPreview;
            OnPlace = onPlace;
        }
    }
}