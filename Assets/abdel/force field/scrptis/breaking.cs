using UnityEngine;

public class breaking : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("fire")) return;
        Destroy(gameObject);
    }
}
