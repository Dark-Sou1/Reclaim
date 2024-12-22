using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class BGIconSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject iconPrefab;
    [SerializeField] protected float distance = 4;
    [SerializeField] protected Vector2 offset;
    [SerializeField] protected List<Vector2Int> ignoreTiles;
    [SerializeField] protected Vector2Int size;

    void Start()
    {
        SpawnTiles();
    }


    protected virtual void SpawnTiles()
    {
        Vector2Int p;
        for (int x = -size.x / 2; x < size.x / 2; x++)
        {
            for (int y = -size.y / 2; y < size.y / 2; y++)
            {
                Vector3 spawnPos = new Vector3(x * distance + offset.x, y * distance + offset.y, 0);
                p = new Vector2Int(x, y);
                if (!ignoreTiles.Contains(p))
                    Instantiate(iconPrefab, spawnPos, Quaternion.identity, transform);
            }
        }

        //hex grid
        /*for (int x = -size.x / 2; x < size.x / 2; x++) 
        {
            for (int y = -size.y / 2; y < size.y / 2; y++)
            {
                float offset = y % 2 == 0 ? 0 : (2f/3f*distance);
                Vector3 spawnPos = new Vector3(x * Mathf.Sqrt(3) * (distance * 2 / 3) + offset, y * (3 / 2) * distance, 0);
                p = new Vector2Int(x, y);
                if (!ignoreTiles.Contains(p))
                    Instantiate(iconPrefab, spawnPos, Quaternion.identity);
            }
        }*/
    }
}
