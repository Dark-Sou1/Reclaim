using UnityEngine;

public class AlchemyIconEasterEgg : MonoBehaviour
{
    [SerializeField] Sprite small;
    [SerializeField] Sprite big;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        int r = Random.Range(0, 100);
        if (r < 1)
            spriteRenderer.sprite = small;
        else if (r < 2)
            spriteRenderer.sprite = big;
    }

}
