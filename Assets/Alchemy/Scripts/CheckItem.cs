using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CheckItem : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefabs;
    
    private ItemUIManager itemUIManager;
    public GameObject[] itemTags;

    private void Start()
    {
        itemUIManager = GetComponent<ItemUIManager>();
    }

    private void CreateItemChecklist()
    {
        for (int i = 0; i < itemPrefabs.Length; i++)
        {
            itemTags[i] = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        }
    }
}
