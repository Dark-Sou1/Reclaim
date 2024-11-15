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
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        { Time.timeScale = 1f;
            up.text = "";
            down.text = "";    
        }
        point.text = add +"";
    }
    // Update is called once per frame
    public void Gameover()
    {
        deathscreen.SetActive(true);
        deathtext.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Point()
    {
        add += 1;  
    }
}
