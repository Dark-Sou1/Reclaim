using Giacomo;
using System.Collections.Generic;
using UnityEngine;

public enum Unlockable { ExplodingTower, FireTower, FreezeTower, StunTower, BoostTower, PoisonPotion, FireballPotion, WeaknessPotion }
public class TowerDefenseUnlocks : MonoBehaviour
{
    protected static List<Unlockable> unlocked = new List<Unlockable>();
    
    public List<Unlockable> debugUnlock;

    public GameObject fireTower;
    public GameObject stunTower;
    public GameObject boostTower;
    public GameObject freezeTower;
    public GameObject explodingTower;

    public GameObject poisonPotion;
    public GameObject fireballPotion;
    public GameObject weaknessPotion;
    
    public static void UnlockTower(Unlockable unlock)
    {
        if(!unlocked.Contains(unlock))
            unlocked.Add(unlock);
    }


    private void Awake()
    {
        foreach (Unlockable u in debugUnlock)
            unlocked.Add(u);

        fireTower.SetActive(unlocked.Contains(Unlockable.FireTower));
        stunTower.SetActive(unlocked.Contains(Unlockable.StunTower));
        boostTower.SetActive(unlocked.Contains(Unlockable.BoostTower));
        freezeTower.SetActive(unlocked.Contains(Unlockable.FreezeTower)); 
        explodingTower.SetActive(unlocked.Contains(Unlockable.ExplodingTower));

        poisonPotion.SetActive(unlocked.Contains(Unlockable.PoisonPotion));
        fireballPotion.SetActive(unlocked.Contains(Unlockable.FireballPotion));
        weaknessPotion.SetActive(unlocked.Contains(Unlockable.WeaknessPotion));
    }
}
