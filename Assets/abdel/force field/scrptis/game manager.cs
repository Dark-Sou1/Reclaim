using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class shildmanager : MonoBehaviour
{
    public static shildmanager instance;
    public TextMeshProUGUI explain;
    public GameObject deathscreen;
    public GameObject deathtext;

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
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Time.timeScale = 1f;
            explain.text = "";

        }
    }
    // Update is called once per frame
    public void gameover()
    {
        deathscreen.SetActive(true);
        deathtext.SetActive(true);
        Time.timeScale = 0f;
    }
}