using System.Collections;
using UnityEngine;

public class FlipCard : MonoBehaviour
{
    [SerializeField] Sprite cardBack;
    [SerializeField] Sprite cardFront;

    private SpriteRenderer cardSpriteRenderer;
    private bool isCardFlipped = false;
    private CardManager cardManager;
    private bool isFlipping = false;
    private AudioSource audioSource;


    void Start()
    {
        cardSpriteRenderer = GetComponent<SpriteRenderer>();
        cardManager = FindAnyObjectByType<CardManager>();
        audioSource = FindAnyObjectByType<AudioSource>();

    }

    private void OnMouseDown()
    {
        if (!isFlipping && !isCardFlipped && (cardManager.selected[0] == null || cardManager.selected[1] == null || cardManager.selected[2] == null))
        {
            StartCoroutine(FlipCardToFront());
            cardManager.AddToSelection(gameObject);
        }
    }

    public IEnumerator FlipCardToFront()
    {
        isFlipping = true;
        audioSource.Play();

        yield return StartCoroutine(RotateOverTime(new Vector3(0, 90, 0)));
        cardSpriteRenderer.sprite = cardFront;

        yield return StartCoroutine(RotateOverTime(new Vector3(0, 90, 0)));

        isCardFlipped = true;
        isFlipping = false;
    }

    public IEnumerator FlipCardToBack()
    {
        isFlipping = true;
        audioSource.Play();

        yield return StartCoroutine(RotateOverTime(new Vector3(0, 90, 0)));
        cardSpriteRenderer.sprite = cardBack;

        yield return StartCoroutine(RotateOverTime(new Vector3(0, 90, 0)));

        isCardFlipped = false;
        isFlipping = false;
    }
    public IEnumerator FlipCardBackAndForth()
    {
        yield return StartCoroutine(RotateOverTime(new Vector3(0, 90, 0)));
        cardSpriteRenderer.sprite = cardFront;
        yield return StartCoroutine(RotateOverTime(new Vector3(0, 90, 0)));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(RotateOverTime(new Vector3(0, 90, 0)));
        cardSpriteRenderer.sprite = cardBack;
        yield return StartCoroutine(RotateOverTime(new Vector3(0, 90, 0)));


    }
    private IEnumerator RotateOverTime(Vector3 rotationAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(rotationAmount);

        float elapsedTime = 0f;
        float duration = 0.15f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }

    public void ResetCard()
    {
        if (isCardFlipped)
        {
            StartCoroutine(FlipCardToBack());
        }
    }
}
