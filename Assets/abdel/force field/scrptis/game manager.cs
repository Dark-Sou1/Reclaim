using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class shildmanager : MonoBehaviour
{
    public static shildmanager instance;
    public TextMeshProUGUI explain;
    public GameObject deathscreen;
    public GameObject deathtext;
    public GameObject wintext;
    public GameObject winbtn;
    public GameObject loosebtn;
    public GameObject[] debrisObjects;
    public UnityEngine.Events.UnityEvent onAllDebrisDestroyed;
    public bool yay = false;
    public bool loose = false;


    void Awake()
    {
        Time.timeScale = 0f;
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        Time.timeScale = 0f;
        winbtn.SetActive(false);
        deathscreen.SetActive(false);
        deathtext.SetActive(false);
        wintext.SetActive(false);
        loosebtn.SetActive(false);
        debrisObjects = GameObject.FindGameObjectsWithTag("debris");
    }

    void Update()
    {
      if (loose == true)
      {
          return; 
      }
      else
      {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
           Time.timeScale = 1f;
           explain.text = "";

        }
        if (AreAllDebrisDestroyed())
        {
           Debug.Log("All debris have been destroyed!");
           win();
           onAllDebrisDestroyed.Invoke();
        }
      }
      
    }
    private bool AreAllDebrisDestroyed()
    {
        foreach (GameObject debris in debrisObjects)
        {
            if (debris != null) 
                return false;
        }
        return true; 
    }
    public void gameover()
    {
        if (yay == true)
        {
            return;
        }
        else
        {
        deathscreen.SetActive(true);
        deathtext.SetActive(true);
        Time.timeScale = 0f;
        loose = true;
        loosebtn.SetActive(true);
        }
        
    }
    public void win()
    {
        if (loose == true)
        {
            return;
        }
        else
        {
        deathscreen.SetActive(true);
        wintext.SetActive(true);
        yay = true;
        winbtn.SetActive(true);
        }
        
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