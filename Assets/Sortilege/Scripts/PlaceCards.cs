using UnityEngine;

public class PlaceCards : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject[] cards;
    [SerializeField] Transform[] positions;

    int[] randomIndices;

    private void Start()
    {
        randomIndices = new int[cards.Length];
        for (int i = 0; i < randomIndices.Length; i++)
        {
            randomIndices[i] = i;
        }

        for (int i = randomIndices.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = randomIndices[i];
            randomIndices[i] = randomIndices[j];
            randomIndices[j] = temp;
        }
    }

    private void Update()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (Vector3.Distance(cards[i].transform.position, positions[randomIndices[i]].position) > 0.01f)
            {
                cards[i].transform.position = Vector3.MoveTowards(cards[i].transform.position,positions[randomIndices[i]].position,speed * Time.deltaTime);
            }
        }
    }
}
