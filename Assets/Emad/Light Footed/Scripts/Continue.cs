using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
