using UnityEngine;
using UnityEngine.XR;
namespace Emad
{

    public class Jumping : MonoBehaviour

    {
        public Rigidbody2D rb2;
        
        public bool isJumping = false;
        

        void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.CompareTag("ground"))
            {
                isJumping = false;

            }
            else
                isJumping = true;

        }

        // Update is called once per frame
        void Update()
        {
            
            if (isJumping == false)
            {
                
                float yInput = Input.GetAxis("Vertical");

                if (Mathf.Abs(yInput) > 0)
                {
                    rb2.linearVelocity = new Vector2(rb2.linearVelocity.x, yInput * 10);
                }
            }
        }
    }
}
