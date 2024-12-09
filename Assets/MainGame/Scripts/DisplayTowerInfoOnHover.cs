using Giacomo;
using UnityEngine;

public class DisplayTowerInfoOnHover : Interactable2D
{
    public Tower tower;

    protected override void OnCursorEnter()
    {
        DisplayInfoUI.Instance.Show(this, tower.shopIcon, tower.towerName, tower.towerDescription);
    }

    protected override void OnCursorExit()
    {
        DisplayInfoUI.Instance.Hide(this);
    }
}
