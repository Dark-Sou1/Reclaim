using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;

public class QTE : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI pass;
    [SerializeField] Animator animator;
    [SerializeField] GameObject flame;
    [SerializeField] ParticleSystem flames;
    [SerializeField] GameObject amogous;
    [SerializeField] GameObject inputText;
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;
    public int QTEGen;
    public int waitingInput;
    public int correctInput;
    public int countDown;
    public int rounds = 4;
    public bool fireThrow = false;

    private void Update()
    {
        if (waitingInput == 0)
        {
            QTEGen = Random.Range(1, 5);
            countDown = 1;

            if (QTEGen == 1)
            {
                waitingInput = 1;
                text.SetText("{F}");
            }
            if (QTEGen == 2)
            {
                waitingInput = 1;
                text.SetText("{I}");
            }
            if (QTEGen == 3)
            {
                waitingInput = 1;
                text.SetText("{R}");
            }
            if (QTEGen == 4)
            {
                waitingInput = 1;
                text.SetText("{E}");
            }
        }
        if (QTEGen == 1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    correctInput = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctInput = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    correctInput = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctInput = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    correctInput = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctInput = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 4)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    correctInput = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctInput = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
    }
    IEnumerator KeyPressing()
    {
        QTEGen = 7;
        if (correctInput == 1) 
        {
            countDown = 2;
            pass.SetText("YES!");
            flame.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            correctInput = 0;
            pass.SetText("");
            text.SetText("");
            yield return new WaitForSeconds(0.2f);
            waitingInput = 0;
            countDown = 2;
            rounds --;


            if (rounds == 0)
            {
                animator.SetBool("Throw", true);
                yield return new WaitForSeconds(0.2f);
                //flame.SetActive(false);
                flames.Play();
                yield return new WaitForSeconds(0.5f);
                amogous.SetActive(false);
                yield return new WaitForSeconds(0.2f);
                //inputText.SetActive(false);
                pass.SetText("YOU SLAYED DA MONSTA !");
                text.SetText("YOOOO !!!!");
                win.SetActive(true);
                Time.timeScale = 0;
            }

        }
        QTEGen = 7;
        if (correctInput == 2)
        {
            countDown = 1;
            text.SetText("You ded");
            pass.SetText("BRUH !!");
            lose.SetActive(true);
            Time.timeScale = 0;
        // yield return new WaitForSeconds(0.2f);
        // correctInput = 0;
        // pass.SetText("");
        // text.SetText("");
        // yield return new WaitForSeconds(0.2f);
        // waitingInput = 0;
        // countDown = 1;
        // Time.timeScale = 0;
        }

    }


}

