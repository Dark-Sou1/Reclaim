using UnityEngine;

public class fireball : MonoBehaviour
{
    public Rigidbody2D fire { get; set;}
    public float speed = 0f;

    void Awake()
    {
      // fire = GetComponent<Rigidbody2D>(); 

    }
    void Start()
    {
        Invoke(nameof(SetRandomTrajectory), 1f);
        fire = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1;

        fire.AddForce(force.normalized * speed);
    }
}
