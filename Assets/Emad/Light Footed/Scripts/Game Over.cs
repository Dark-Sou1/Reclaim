using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Ennemy"))
        {
            text.SetText("Game Over");

            Time.timeScale = 0;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
