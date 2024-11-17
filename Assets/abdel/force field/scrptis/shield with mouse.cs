using Sirenix.Utilities;
using UnityEngine;

public class shieldwithmouse : MonoBehaviour
{
   public Vector3 mousePosition;
   public Rigidbody2D shield;
    public Vector2 ThisWay;
    public float speed = 0f;

    void Start()
    {
        shield = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) 
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ThisWay = (mousePosition - transform.position).normalized;
            shield.linearVelocity = new Vector2(ThisWay.x * speed, ThisWay.x * speed);
        }
        else
        {
            shield.linearVelocity = Vector2.zero;
        }
    }
}
