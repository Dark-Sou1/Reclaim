using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] cardPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(cardPrefabs[i], spawnPoints[i].position, Quaternion.identity);
        }

    }

}
