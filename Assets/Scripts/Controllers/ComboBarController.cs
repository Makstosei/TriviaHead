using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ComboBarController : MonoBehaviour
{
    private bool comboAnimPlaying;
    public int comboint;
    AnswerManager answerManager;

    private void OnEnable()
    {
        EventManager.onHitWall += HitWallEvent;
    }
    private void OnDisable()
    {
        EventManager.onHitWall -= HitWallEvent;
    }



    void CombotextAnim()
    {
        gameObject.transform.GetChild(0).GetComponent<Animator>().Play("ComboText");
    }

    private void Start()
    {
        answerManager = FindObjectOfType<AnswerManager>();
    }


    void HitWallEvent(bool isFinishLine)
    {
        if (!isFinishLine)
        {
            if (answerManager.isAnswerCorrect)
            {
                if (comboint + 1 <= 5)
                {
                    CombotextAnim();
                    comboint++;
                    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x" + comboint.ToString();
                }
            }
            else
            {
                if (comboint - 1 <= 0)
                {
                    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                    comboint = 0;
                }
                else
                {
                    comboint--;
                    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "x" + comboint.ToString();
                    CombotextAnim();
                }
            }
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";

        }

    }

}
