using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class gameover : MonoBehaviour
{
    public static gameover instance;

    public TextMeshProUGUI point;
    public TextMeshProUGUI up;
    public TextMeshProUGUI down;

    public GameObject deathscreen;
    public GameObject deathtext;
    public GameObject wintext;

    bool End = false;

    public int add;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {  
        deathscreen.SetActive(false);
        Time.timeScale = 0f;
        deathtext.SetActive(false);
        wintext.SetActive(false);
    }

    void Update()
    {
        if (End)
            return;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            up.text = "";
            down.text = "";
        }

        point.text = add + "";
    
        if (add > 9)
        {
            win();
        }
    }

    public void Gameover()
    {
        deathscreen.SetActive(true);
        deathtext.SetActive(true);
        End = true;
        Time.timeScale = 0f;
    }
    public void Point()
    {
        add += 1;  
    }
    public void win()
    {
        wintext.SetActive(true);
        deathscreen.SetActive(false);
        End = true;
        Time.timeScale = 0f;
    }
}
