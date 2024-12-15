using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, (int required, int collected)> itemGoals = new Dictionary<string, (int, int)>();

    [SerializeField] private Image[] appleImages;
    [SerializeField] private Image[] mangoImages;
    [SerializeField] private Image[] pineappleImages;
    
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;

    private bool apple;
    private bool mango;
    private bool pineapple;
    
    private SpawnManager spawnManager;
    
    [SerializeField] Image[] healthBars;
    
    
    public int currentHealth;
    

    void Start()
    {
        spawnManager = FindAnyObjectByType<SpawnManager>();
        itemGoals["AetherFruit"] = (3, 0);
        itemGoals["Philosopher's Core"] = (3, 0);
        itemGoals["Elixir Berries"] = (3, 0);

        currentHealth = healthBars.Length;
        SetImagesColor(appleImages, Color.gray);
        SetImagesColor(mangoImages, Color.gray);
        SetImagesColor(pineappleImages, Color.gray);
    }



    public void CollectItem(string itemName)
    {
        if (itemGoals.ContainsKey(itemName))
        {
            // Increment the collected count
            var goal = itemGoals[itemName];
            if (goal.collected < goal.required)
            {
                goal.collected++;
                itemGoals[itemName] = goal;

                // Update the UI for the collected item
                UpdateCollectedImages(itemName, goal.collected);

                // Check if over-collected
                if (goal.collected > goal.required)
                {
                    Debug.Log("Over-collected " + itemName);
                    ReduceHealth();
                }
            }
            else
            {
                // Over-collection penalty
                ReduceHealth();
            }
        }
        apple = true;
        mango = true;
        pineapple = true;
        
        foreach (var img in appleImages)
        {
            if (img.color == Color.gray)
            {
                apple = false;
            }
        }
        foreach (var img in mangoImages)
        {
            if (img.color == Color.gray)
            {
                mango = false;
            }
        }
        foreach (var img in pineappleImages)
        {
            if (img.color == Color.gray)
            {
                pineapple = false;
            }
        }

        if (mango && apple && pineapple)
        {
            gameWinPanel.SetActive(true);
        }
    }
    void UpdateCollectedImages(string itemName, int collected)
    {
        Image[] itemImages = GetItemImages(itemName);

        if (itemImages != null && collected - 1 < itemImages.Length)
        {
            itemImages[collected - 1].color = Color.white; // Change the collected image to white
        }
    }
    Image[] GetItemImages(string itemName)
    {
        return itemName switch
        {
            "AetherFruit" => appleImages,
            "Elixir Berries" => mangoImages,
            "Philosopher's Core" => pineappleImages,
            _ => null,
        };
    }
    void SetImagesColor(Image[] images, Color color)
    {
        foreach (Image img in images)
        {
            img.color = color;
        }
    }
    void ReduceHealth()
    {
        print(currentHealth);
        print(healthBars.Length);
        if (currentHealth > 0)
        {
            healthBars[currentHealth-1].enabled = false;
            currentHealth--;
        }

        if (currentHealth <= 0)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        Destroy(spawnManager.gameObject);
        SceneManager.LoadScene(0);
    }
    public void EndGame()
    {
        Destroy(spawnManager.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}