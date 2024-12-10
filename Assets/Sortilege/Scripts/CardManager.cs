using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] selected;
    public GameObject[] cards;
    private AudioSource audioSource;
    public int life = 3;
    float timer;
    float timeTillFlip = 0.5f;
    bool startTimer;
    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        selected = new GameObject[3];
        StartCoroutine(OnStart());

    }
    private void Update()
    {
        if (selected[0] != null && selected[1] != null && selected[2] != null)
        {
            print(1);
            if (selected[0].CompareTag(selected[1].tag) && selected[1].CompareTag(selected[2].tag))
            {
                startTimer = true;
            }
            else
            {
                StartCoroutine(ResetAndClearSelection());
            }

        }
        if (startTimer)
        {
            timer += Time.deltaTime;
            print(timer);
        }
        if (timer >= timeTillFlip)
        {
            foreach (GameObject card in selected)
            {
                Destroy(card);
            }
            timer = 0;
            startTimer = false;
            ClearSelection();
        }
    }
    private IEnumerator OnStart()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject card in cards)
        {
            StartCoroutine(card.GetComponent<FlipCard>().FlipCardBackAndForth());
            audioSource.Play();
        }
    }

    private IEnumerator ResetAndClearSelection()
    {
        yield return new WaitForSeconds(timeTillFlip);
        ResetSelectedCards();
        ClearSelection();
        life--;
    }
    public void AddToSelection(GameObject card)
    {
        for (int i = 0; i < selected.Length; i++)
        {
            if (selected[i] == null)
            {
                selected[i] = card;
                break;
            }
        }

    }

    private void ResetSelectedCards()
    {
        foreach (GameObject card in selected)
        {
            if (card != null)
            {
                card.GetComponent<FlipCard>().ResetCard();
            }
        }
    }

    private void ClearSelection()
    {
        for (int i = 0; i < selected.Length; i++)
        {
            selected[i] = null;
        }
    }
}
