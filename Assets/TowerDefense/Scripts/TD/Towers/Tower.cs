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


}
