using UnityEngine;
using UnityEngine.XR;

public class PlayerMov : MonoBehaviour
{
    public int jumpSpeed = 10;
    public Rigidbody2D rb;
    public float speed;
    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    [SerializeField] Animator Wizard;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }
    void Update()
    {
         grounded = Checkground();
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(xInput) > 0)
        {
            rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocity.y);
            Wizard.SetBool("isWalking", true);



        }
        else
        {
            Wizard.SetBool("isWalking", false);
        }
        if (Mathf.Abs(yInput) > 0 && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x , jumpSpeed);
            Wizard.SetBool("isWalking", true);
        }
        //else
     //   {
       //     Wizard.SetBool("isWalking", false);
      //  }
        // Vector2 direction = new Vector2(xInput, yInput).normalized;
        // rb.linearVelocity = direction * speed;


    }

    bool Checkground()
    {
        Debug.DrawLine(transform.position + new Vector3(0, -1.6f, 0), transform.position + new Vector3(0, -4f, 0));
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -1.6f, 0), Vector2.down, .1f);
        if (hit)
        {
            Debug.Log(hit.collider.name);

            return true;
        }
        return false;

    }
} 
    

    



