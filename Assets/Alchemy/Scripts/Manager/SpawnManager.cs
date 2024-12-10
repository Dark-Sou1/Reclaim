using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float timeBetweenSpawns;
    [SerializeField] private Transform transformPositionLeft;
    [SerializeField] private Transform transformPositionRight;
    [SerializeField] private GameObject[] spawnPrefabs;

    private int randomRangeInt;
    private float randomXPosition;
    private float transformYPosition;
    public float timer;

    private void Start()
    {
        //initializes y position to not call transform everytime
        transformYPosition = transformPositionRight.position.y; 
    }

    private void Update()
    {
        if (CheckTimer())
        {
            Instantiate(RandomItem(), RandomPosition(), Quaternion.identity); //spawn item
        }
    }


    bool CheckTimer()// returns true if you can spawn an item
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenSpawns)
        {
            timer = 0;
            return true;
        }
        return false;
    }
    GameObject RandomItem() // returns random prefab from array of prefabs
    {
        randomRangeInt = Random.Range(0, spawnPrefabs.Length);
        return spawnPrefabs[randomRangeInt];
    }

    Vector2 RandomPosition() // Returns random x position between two transform points
    {
        randomXPosition = Random.Range(transformPositionLeft.position.x, transformPositionRight.position.x);
        return new Vector2(randomXPosition,transformYPosition);
    }
}
