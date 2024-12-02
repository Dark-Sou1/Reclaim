using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

namespace Giacomo
{
    public class Tower : Interactable2D
    {

        public Tile Tile { get; set; }
        [HideInInspector] public Stats stats;
        [HideInInspector] public EffectHandler effects;

        [Header("General")]
        public string towerName;
        [TextArea]
        public string towerDescription;
        public int cost;
        public Sprite placingPreview;
        public Sprite shopIcon;
        public float b_maxRange = 3;
        public float b_minRange = 0;

        public List<Transform> scaleWithMaxRange = new List<Transform>();
        public List<Transform> scaleWithMinRange = new List<Transform>();

        [SerializeField, HideInInspector] Transform minRangeIndicator;
        [SerializeField, HideInInspector] Transform maxRangeIndicator;

        protected bool placedThisFrame = true;

        protected override void ManagedInitialize()
        {
            if(!stats)
                stats = gameObject.AddComponent<Stats>();
            if(!effects)
                effects = gameObject.AddComponent<EffectHandler>();

            stats.AddStat("maxRange", b_maxRange, 0);
            stats.AddStat("minRange", b_minRange, 0);
            stats["maxRange"].OnValueChanged += UpdateRangeIndicators;
            stats["minRange"].OnValueChanged += UpdateRangeIndicators;
            SetupRangeIndicators();
        }

        public override void ManagedLateUpdate()
        {
            if (placedThisFrame)
                placedThisFrame = false;
        }

        protected void SetupRangeIndicators()
        {
            Color lineColor = new Color(1, 1, 1, .5f);
            if(maxRangeIndicator == null)
            {
                maxRangeIndicator = new GameObject("MaxRangeIndicator").transform;
                maxRangeIndicator.parent = transform;
                maxRangeIndicator.transform.position = transform.position;
                var maxRangeLine = maxRangeIndicator.gameObject.AddComponent<LineRenderer>();
                maxRangeLine.startColor = lineColor;
                maxRangeLine.endColor = lineColor;
                maxRangeLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
                CircularLineRenderer.DrawCircle(maxRangeLine, 1, 0, .03f, Vector2.one/2, 90);
                scaleWithMaxRange.Add(maxRangeIndicator);
            }
            if(minRangeIndicator == null)
            {
                minRangeIndicator = new GameObject("MinRangeIndicator").transform;
                minRangeIndicator.parent = transform;
                minRangeIndicator.transform.position = transform.position;
                var minRangeLine = minRangeIndicator.gameObject.AddComponent<LineRenderer>();
                minRangeLine.startColor = lineColor;
                minRangeLine.endColor = lineColor; 
                minRangeLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
                CircularLineRenderer.DrawCircle(minRangeLine, 1, 0, .03f, Vector2.one/2, 90);
                scaleWithMinRange.Add(minRangeIndicator);
            }
            maxRangeIndicator.gameObject.SetActive(false);
            minRangeIndicator.gameObject.SetActive(false);
            UpdateRangeIndicators(null);
        }

        protected void UpdateRangeIndicators(Stat.StatValueChangedEventArgs args)
        {
            float max = stats["maxRange"] * 2;
            foreach(Transform t in scaleWithMaxRange)
                t.localScale = Vector3.one * max;
            
            float min = stats["minRange"] * 2;
            foreach(Transform t in scaleWithMinRange)
                t.localScale = Vector3.one * min;
        }

        protected override void OnCursorEnter()
        {
            maxRangeIndicator.gameObject.SetActive(true);
            minRangeIndicator.gameObject.SetActive(true);
        }

        protected override void OnCursorExit()
        {
            maxRangeIndicator.gameObject.SetActive(false);
            minRangeIndicator.gameObject.SetActive(false);
        }

        protected override void OnCursorSelectStart()
        {
            if (!InputManager.Instance.acceptInput || placedThisFrame)
                return;

            InputManager.Instance.SetMovingStatus(true);
            this.Tile.tower = null; //hacky way of doing it. Works for now
            TowerPlacementManager.Instance.StartPlacing(this, OnMoveAway, OnCancelMoving);
            gameObject.SetActive(false);
        }

        protected void OnMoveAway()
        {
            Destroy(gameObject);
            InputManager.Instance.SetMovingStatus(false);
        }

        protected void OnCancelMoving()
        {
            gameObject.SetActive(true);
            this.Tile.PlaceTower(this);
            InputManager.Instance.SetMovingStatus(false);
        }

    }
}