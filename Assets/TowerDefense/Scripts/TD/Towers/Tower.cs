using Giacomo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Tile Tile { get; set; }
    [HideInInspector] public Stats stats;
    [HideInInspector] public EffectHandler effects;
    
    public int cost;
    public Sprite placingPreview;
    public Sprite shopIcon;

    void Awake()
    {
        stats = gameObject.AddComponent<Stats>();
        effects = gameObject.AddComponent<EffectHandler>();

        Initialize();
    }

    protected virtual void Initialize() { }

    protected void OnMouseDrag()
    {
        if(!InputManager.Instance.acceptInput)
            return;
        InputManager.Instance.SetMovingStatus(true);
        this.Tile.tower = null;
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
