using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Giacomo
{

    public class TowerPlacementManager : Singleton<TowerPlacementManager>
    {
        public SpriteRenderer previewRenderer;

        [ShowInInspector, ReadOnly] public Tower placingTower { get; private set; }
        protected Action onPlace;
        protected Action cancelPlacing;

        bool startedPlacingThisFrame;
        public void Update()
        {
            if (startedPlacingThisFrame)
            {
                startedPlacingThisFrame = false;
                return;
            }

            if(placingTower == null)
                return;

            if (Input.GetKey(KeyCode.Escape))
            { 
                cancelPlacing?.Invoke();
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
                    onPlace?.Invoke();

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
            onPlace = null;
            cancelPlacing = null;
            previewRenderer.enabled = false;
            InputManager.Instance.SetPlacingStatus(false);
        }

        public void StartPlacing(Tower tower, Action onPlace = null, Action cancelPlacing = null)
        {
            if (placingTower == tower)
            {
                StopPlacing();
                return;
            }

            InputManager.Instance.SetPlacingStatus(true);
            placingTower = tower;
            previewRenderer.sprite = placingTower.placingPreview;
            this.onPlace = onPlace;
            this.cancelPlacing = cancelPlacing;
            startedPlacingThisFrame = true;
        }
    }
}