using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] float delay;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
