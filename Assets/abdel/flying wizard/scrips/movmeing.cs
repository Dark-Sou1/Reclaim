using UnityEngine;

public class movmeing : MonoBehaviour
{
    [SerializeField] public float speed = 0f;

    // Update is called once per frame
    void Update()
    {
      transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
