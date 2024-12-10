using UnityEngine;

public class PotMovement : MonoBehaviour
{
    [SerializeField] private float boundLeft;
    [SerializeField] private float boundRight;
    private Vector2 mousePosition;
    private Vector2 translatedPosition;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main; //initialize camera in case of possible null references
    }

    private void OnMouseDrag()
    {
        mousePosition = Input.mousePosition; // position of mouse on screen
        translatedPosition = cam.ScreenToWorldPoint(mousePosition); // position of mouse as world position
        if (translatedPosition.x < boundLeft)// boundaries for pot
        {
            translatedPosition.x = boundLeft;
        }
        else if (translatedPosition.x > boundRight)
        {
            translatedPosition.x = boundRight;
        }
        
        transform.position = new Vector2(translatedPosition.x, transform.position.y); // moves the pot
    }
}
