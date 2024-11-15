using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class gameover : MonoBehaviour
{
    public static gameover instance;
    TextMeshProUGUI up;
    TextMeshProUGUI down;
    public GameObject deathscreen;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {  
        deathscreen.SetActive(false);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        { Time.timeScale = 1f;
            up.text = "";
            down.text = "";    
        }

    }
    // Update is called once per frame
    public void Gameover()
    {
        deathscreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
