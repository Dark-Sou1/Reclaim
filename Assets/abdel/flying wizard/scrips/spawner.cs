using UnityEngine;

public class spawner : MonoBehaviour
{
    public float MaxTime = 0f;
    public float HightRange = 0f;
    public float timer;
    public GameObject obstical;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        if (timer > MaxTime)
            {
            Spawn();
            timer = 0;
        }
        timer += Time.deltaTime;

    }
    // Update is called once per frame
    void Spawn()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-HightRange, HightRange));
        GameObject pipe = Instantiate(obstical, spawnPos, Quaternion.identity);
        Destroy(pipe, 5f);
    }
}
