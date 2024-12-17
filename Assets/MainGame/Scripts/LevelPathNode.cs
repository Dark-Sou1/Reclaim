using System.Collections.Generic;
using System;
using UnityEngine;
using Giacomo;
using UnityEditor;

[Serializable]
public class LevelPathNode : Interactable2D
{
    public string minigameScene;

    public DisplayableInfo minigameInfo;
    public DisplayableInfo rewardInfo;
    public Unlockable unlocks;

    protected override void ManagedInitialize()
    {
        var r = GetComponent<SpriteRenderer>();
        r.sprite = minigameInfo.icon;
    }

    //public List<GameObject> previousPaths;
    bool isEnabled;

    protected override void OnCursorEnter()
    {
        DisplayInfoUI.Instance.Show(this, rewardInfo.icon, rewardInfo.title, rewardInfo.description);
        MinigameInfoUI.Instance.Show(this, minigameInfo.icon, minigameInfo.title, minigameInfo.description);
    }

    protected override void OnCursorExit()
    {
        DisplayInfoUI.Instance.Hide(this);
        MinigameInfoUI.Instance.Hide(this);
    }

    public void Enable()
    {
        var scaleAnim = GetComponent<SineWaveScale>();
        scaleAnim.enabled = true;
        var r = GetComponent<SpriteRenderer>();
        r.color = Color.white;
        isEnabled = true;
    }

    protected override void OnCursorSelectEnd()
    {
        if(isEnabled)
            LevelPath.Instance.ClickedNode(this);
    }
}