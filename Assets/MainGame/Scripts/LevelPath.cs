using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPath : Singleton<LevelPath>
{
    public LevelPathNodeChoice[] rows;


    public static List<LevelPathNode> chosenNodes = new List<LevelPathNode>();
    public static int currentLevel => chosenNodes.Count;

    private void Start()
    {
        foreach (var node in rows[currentLevel].Nodes)
        {
            node.Enable();
        }
    }

    public void ClickedNode(LevelPathNode node)
    {
        if (rows[currentLevel].Nodes.Contains(node))
        {
            chosenNodes.Add(node);
            TowerDefenseUnlocks.UnlockTower(node.unlocks);
            SceneManager.LoadScene(node.minigameScene);
        }
    }

}

[Serializable]
public class LevelPathNodeChoice
{
    public List<LevelPathNode> Nodes;
}

