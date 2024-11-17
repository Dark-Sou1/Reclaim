using UnityEngine;

public class movment : MonoBehaviour
{
    public Rigidbody2D shield;
    public Vector2 ThatWay;
    public float speed = 0f;
    void Start()
    {
        shield = GetComponent<Rigidbody2D>();
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ThatWay = Vector2.left;
            Time.timeScale = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow))
        {  
            ThatWay = Vector2.right;
            Time.timeScale = 1f;
        }
        else
        {
            ThatWay = Vector2.zero;
            Time.timeScale = 1f;
        }
    }
    void FixedUpdate()
    {
       if (ThatWay == Vector2.zero)
        { 
            return; 
        }
       shield.AddForce(ThatWay * speed);
    }
}
