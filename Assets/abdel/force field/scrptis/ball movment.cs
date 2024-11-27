using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
public class Ball : MonoBehaviour
{
    public Rigidbody2D fire { get; set; }
    public float speed = 0f;

    void Start()
    {
        fire = GetComponent<Rigidbody2D>();
        Invoke(nameof(SetRandomTraject), 1f);
    }

    void SetRandomTraject()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;
    fire.AddForce(force.normalized * speed);
    }
}