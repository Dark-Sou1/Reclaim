using System;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    private ItemUIManager itemUIManager;
    private void Start()
    {
        itemUIManager = GetComponent<ItemUIManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        itemUIManager.UpdateItemBar(other.gameObject);
        Destroy(other.gameObject);
    }
}
