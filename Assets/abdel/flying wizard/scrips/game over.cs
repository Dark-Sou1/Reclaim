using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour
{
    public static gameover instance;

    public TextMeshProUGUI point;
    public TextMeshProUGUI up;
    public TextMeshProUGUI down;

    public GameObject deathscreen;
    public GameObject deathtext;
    public GameObject wintext;
    public GameObject looseB;
    public GameObject winB;
    bool End = false;

    public int add;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        Time.timeScale = 0f;
        looseB.SetActive(false);
        winB.SetActive(false);
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
        looseB.SetActive(true);
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
        winB.SetActive(true);
        wintext.SetActive(true);
        deathscreen.SetActive(false);
        End = true;
        Time.timeScale = 0f;
    }
    public void tryagain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Continue()
    {
        SceneManager.LoadScene(0);
    }
}
