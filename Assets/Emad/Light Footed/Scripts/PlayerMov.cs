using UnityEngine;
using UnityEngine.XR;

public class PlayerMov : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }
    void Update()
    {

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(xInput) > 0)
        {
            rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocity.y);
        }

        if (Mathf.Abs(yInput) > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, yInput * 10);
        }
        // Vector2 direction = new Vector2(xInput, yInput).normalized;
        // rb.linearVelocity = direction * speed;

        
    }

    void Checkground()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;

    }
} 
    

    



