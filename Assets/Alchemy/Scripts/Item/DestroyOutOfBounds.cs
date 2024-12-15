using System;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
    }
}
