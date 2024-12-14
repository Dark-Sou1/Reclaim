using Emad;
using UnityEngine;
using TMPro;

public class Goal: MonoBehaviour
{
    public bool isGoal;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject next;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("goal"))
        {
            text.SetText("Congtratulations");
            next.SetActive(true);

           Time.timeScale = 0;
            
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
