using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


namespace Giacomo
{

    public class TowerPlacementManager : Singleton<TowerPlacementManager>
    {
        public GameObject preview;
        SpriteRenderer[] previewRenderers;

        [ShowInInspector, ReadOnly] 
        public Tower placingTower { get; private set; }

        protected Action onPlace;
        protected Action cancelPlacing;

        [SerializeField]
        protected LineRenderer rangePreview;


        bool startedPlacingThisFrame;
        public void Update()
        {
            if(placingTower == null)
                return;

            //prevent placing the tower in the same frame it's being selected
            if (startedPlacingThisFrame)
            {
                startedPlacingThisFrame = false;
                return;
            }

            //cancel placing
            if (Input.GetKey(KeyCode.Escape))
            { 
                cancelPlacing?.Invoke();
                StopPlacing();
                return;
            }


            //get current tile
            var mousePos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
            var intCoords = GridManager.FixCoordinates(mousePos);
            Tile hoveringTile = GridManager.Instance.Get(intCoords);

            //place tower
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


            //draw preview
            Vector3 coords = new Vector3(intCoords.x, intCoords.y);
            rangePreview.transform.position = coords;
            preview.transform.position = coords;
            if (hoveringTile && hoveringTile.CanPlace())
            {
                previewRenderers.ForEach(x => x.color = Color.white);
            }
            else
            {
                previewRenderers.ForEach(x => x.color = Color.red);
            }
        }

        public void StopPlacing()
        {
            placingTower = null;
            onPlace = null;
            cancelPlacing = null;
            Destroy(preview);
            rangePreview.gameObject.SetActive(false);
            InputManager.Instance.SetPlacingStatus(false);
        }

        public void StartPlacing(Tower tower, Action onPlace = null, Action cancelPlacing = null)
        {
            if (placingTower == tower)
            {
                StopPlacing();
                return;
            }

            float range = tower.b_maxRange*2;
            if (tower.stats)
                range = tower.stats["maxRange"] * 2;
            rangePreview.transform.localScale = Vector3.one * range;
            rangePreview.gameObject.SetActive(true);

            InputManager.Instance.SetPlacingStatus(true);
            placingTower = tower;
            preview = Instantiate(tower.transform.Find("GFX")).gameObject;
            previewRenderers = preview.GetComponentsInChildren<SpriteRenderer>();
            this.onPlace = onPlace;
            this.cancelPlacing = cancelPlacing;
            startedPlacingThisFrame = true;
        }
    }
}