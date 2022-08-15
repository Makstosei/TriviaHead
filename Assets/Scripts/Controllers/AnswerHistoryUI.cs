using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerHistoryUI : MonoBehaviour
{
    public Sprite TrueSprite, FalseSprite,CurrentSprite;
    private int childcounter;
    AnswerManager answerManager;

    private void OnEnable()
    {
        EventManager.onHitWall += HitWallEvent;
    }
    private void OnDisable()
    {
        EventManager.onHitWall -= HitWallEvent;
    }

    private void Start()
    {
        StartCoroutine(GameStarted());
        answerManager = FindObjectOfType<AnswerManager>();
    }


    IEnumerator GameStarted()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        for (int i = 0; i < FindObjectOfType<QuestionManager>().QuestionList.Count; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }


    void HitWallEvent(bool isFinishWall)
    {
        if (!isFinishWall)
        {
            if (answerManager.isAnswerCorrect)
            {
                gameObject.transform.GetChild(childcounter).GetComponent<Image>().sprite = TrueSprite;
            }
            else
            {
                gameObject.transform.GetChild(childcounter).GetComponent<Image>().sprite = FalseSprite;
            }
            childcounter++;
        }
    }


}
