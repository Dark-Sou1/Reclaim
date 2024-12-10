using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, (int required, int collected)> itemGoals = new Dictionary<string, (int, int)>();

    [SerializeField] TextMeshProUGUI appleText;
    [SerializeField] TextMeshProUGUI mangoText;
    [SerializeField] TextMeshProUGUI pineappleText;
    
    [SerializeField] Image[] healthBars;
    
    
    public int currentHealth;
    

    void Start()
    {
        itemGoals["AetherFruit"] = (Random.Range(1, 11), 0);
        itemGoals["Philosopher's Core"] = (Random.Range(1, 11), 0);
        itemGoals["Elixir Berries"] = (Random.Range(1, 11), 0);

        currentHealth = healthBars.Length;
        
        UpdateUI();
    }

    void UpdateUI()
    {
        appleText.text = $"AetherFruit\n{itemGoals["AetherFruit"].required} : {itemGoals["AetherFruit"].collected}";
        mangoText.text = $"Philosopher's Core\n{itemGoals["Philosopher's Core"].required} : {itemGoals["Philosopher's Core"].collected}";
        pineappleText.text = $"Elixir Berries\n{itemGoals["Elixir Berries"].required} : {itemGoals["Elixir Berries"].collected}";
    }

    public void CollectItem(string itemName)
    {
        if (itemGoals.ContainsKey(itemName))
        {
            var goal = itemGoals[itemName];
            goal.collected++;
            itemGoals[itemName] = goal;

            UpdateUI();

            if (goal.collected > goal.required)
            {
                ReduceHealth();
            }
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
            EndGame();
        }
    }
    void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}