using System;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AetherFruit"))
        {
            gameManager.CollectItem("AetherFruit");
            audioSource.Play();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Elixir Berries"))
        {
            gameManager.CollectItem("Elixir Berries");
            audioSource.Play();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Philosopher's Core"))
        {
            gameManager.CollectItem("Philosopher's Core");
            audioSource.Play();
            Destroy(other.gameObject);
        }
    }
}
