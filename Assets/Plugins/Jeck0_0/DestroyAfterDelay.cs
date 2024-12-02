using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] bool disableInstead;

    void OnEnable()
    {
        if (disableInstead)
        {
            Invoke("Disable", delay);
        }
        else
        {
            Destroy(gameObject, delay);
        }
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
