using UnityEngine;
using UnityEngine.InputSystem;
public class flying : MonoBehaviour
{

    [SerializeField] public float _velocity = 1.5f;

     public Rigidbody2D rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            rb.linearVelocity = Vector2.up * _velocity;
    }
}
