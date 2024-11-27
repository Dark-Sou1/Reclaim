using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class shildmanager : MonoBehaviour
{
    public static shildmanager instance;
    public TextMeshProUGUI explain;
    public GameObject deathscreen;
    public GameObject deathtext;
    public GameObject wintext;
   

    void Awake()
    {
        if (instance == null)
            instance = this;
        Time.timeScale = 0f;
    }
    void Start()
    {
        deathscreen.SetActive(false);
        deathtext.SetActive(false);
        wintext.SetActive(false);
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            explain.text = "";

        }
        if (gameObject.tag == "debris" == null)
        {
          win();
        }
    }
    // Update is called once per frame
    public void gameover()
    {
        deathscreen.SetActive(true);
        deathtext.SetActive(true);
        Time.timeScale = 0f;
    }
    public void win()
    {
        deathscreen.SetActive(true);
        wintext.SetActive(true);
    }
}