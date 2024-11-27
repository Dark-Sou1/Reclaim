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
    public GameObject[] debrisObjects;
    public UnityEngine.Events.UnityEvent onAllDebrisDestroyed;
    public bool yay = false;
    public bool loose = false;


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
        }
        
    }
}