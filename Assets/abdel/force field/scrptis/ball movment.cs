using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
public class Ball : MonoBehaviour
{
    public Rigidbody2D fire { get; set; }
    public float speed = 0f;
    public bool moved = false;

    void Start()
    {
        fire = GetComponent<Rigidbody2D>();
        Time.timeScale = 0f;
        
    }

    void Update()
    {
        if (moved == true)
        {
            return;
        }
        else
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Time.timeScale = 1f;
                Invoke(nameof(SetRandomTraject), 1f);
                moved = true;

            }
        }
    }
    void SetRandomTraject()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;
    fire.AddForce(force.normalized * speed);
    }
}