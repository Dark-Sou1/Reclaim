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
        /*for (int i = 0; i < currentLevel; i++)
        {
            //look away, this is terrible but I don't have time to make it decent
            if(i == 0)
                chosenNodes[i].previousPaths[0].SetActive(true);
            else
                chosenNodes[i].previousPaths[rows[i - 1].Nodes.IndexOf(chosenNodes[i - 1])].SetActive(true);
        }*/

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
            SceneManager.LoadScene(node.minigameScene.name);
        }
    }

}

[Serializable]
public class LevelPathNodeChoice
{
    public List<LevelPathNode> Nodes;
}

