using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class QTE : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI pass;
    public int QTEGen;
    public int waitingInput;
    public int correctInput;
    public int countDown;

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
            yield return new WaitForSeconds(0.2f);
            correctInput = 0;
            pass.SetText("");
            text.SetText("");
            yield return new WaitForSeconds(0.2f);
            waitingInput = 0;
            countDown = 2;
        }
        QTEGen = 7;
        if (correctInput == 2)
        {
            countDown = 1;
            text.SetText("You ded");
            pass.SetText("BRUH !!");
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

