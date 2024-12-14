using Giacomo;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugShortcuts : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.M))
                GameStats.Instance?.ModifyCoins(1000);

            if (Input.GetKeyDown(KeyCode.K))
                GameManager.Instance.enemies.ForEach(x => x.Damage(10000));

            if (Input.GetKeyDown(KeyCode.RightArrow))
                Time.timeScale += 1;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                Time.timeScale -= 1;

            if (Input.GetKeyDown(KeyCode.I))
                Debug.Log($"TimeScale: {Time.timeScale} - Paused: {ManagedBehaviour.PauseAll.True} ({string.Join(", ", ManagedBehaviour.PauseAll.GetReferences())})");

            if (Input.GetKeyDown(KeyCode.W))
                SceneManager.LoadScene(0);
        }
    }
}
