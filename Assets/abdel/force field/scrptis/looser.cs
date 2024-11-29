using UnityEngine;

public class looser : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("fire")) return;
        shildmanager.instance.gameover();
    }
}
