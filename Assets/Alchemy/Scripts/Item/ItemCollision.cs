using System;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AetherFruit"))
        {
            gameManager.CollectItem("AetherFruit");
            Destroy(other.gameObject); // Destroy the collected item
        }
        else if (other.CompareTag("Elixir Berries"))
        {
            gameManager.CollectItem("Elixir Berries");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Philosopher's Core"))
        {
            gameManager.CollectItem("Philosopher's Core");
            Destroy(other.gameObject);
        }
    }
}
