using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public List<QuestionsData.Questions> QuestionList;
    public int currentAnswerid;
    public Material Answera, Answerb, Answerc, ghost, abackground, bbackground, cbackground, correctionmat;
    public int TrueAnswerCount, FalseAnswerCount;
    public bool isAnswerCorrect;

    private void OnEnable()
    {
        EventManager.onQuestionSyncronized += QuestionSyncronized;
        EventManager.onHitWall += hitWallEvent;
        EventManager.onLostLevel += LostLevel;
        EventManager.onAnswerChanged += AnswerChangedEvent;

    }

    private void OnDisable()
    {
        EventManager.onHitWall -= hitWallEvent;
        EventManager.onQuestionSyncronized -= QuestionSyncronized;
        EventManager.onLostLevel -= LostLevel;
        EventManager.onAnswerChanged -= AnswerChangedEvent;

    }
    private void Awake()
    {
        TrueAnswerCount = 0;
        QuestionList = FindObjectOfType<QuestionManager>().QuestionList;

    }
    private void Start()
    {
        Answera.CopyPropertiesFromMaterial(abackground);
        Answerb.CopyPropertiesFromMaterial(bbackground);
        Answerc.CopyPropertiesFromMaterial(cbackground);
        Answera.shader = abackground.shader;
        Answerb.shader = bbackground.shader;
        Answerc.shader = cbackground.shader;
    }


    void AnswerChangedEvent()
    {
        if (QuestionList.Count > 0)
        {
            if (currentAnswerid == QuestionList[0].Correct)
            {
                isAnswerCorrect = true;
            }
            else
            {
                isAnswerCorrect = false;
            }
        }
    }

    void LostLevel()
    {
        StartCoroutine(LostlevelEnum());
    }

    IEnumerator LostlevelEnum()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Answera.CopyPropertiesFromMaterial(abackground);
        Answerb.CopyPropertiesFromMaterial(bbackground);
        Answerc.CopyPropertiesFromMaterial(cbackground);
        Answera.shader = abackground.shader;
        Answerb.shader = bbackground.shader;
        Answerc.shader = cbackground.shader;
    }



    void QuestionSyncronized()
    {
        StartCoroutine(ChangeAnswersImages());
    }



    void hitWallEvent(bool isFinishWall)
    {
        if (isFinishWall)
        {
            TrueAnswerCount--;
            if (TrueAnswerCount < 1)
            {
                EventManager.Instance.EndGameEvent();
            }
        }
        else
        {
            if (isAnswerCorrect)
            {
                TrueAnswerCount++;
                PlayerPrefs.SetInt("TrueCount", TrueAnswerCount);
            }
            else
            {
                FalseAnswerCount++;
                PlayerPrefs.SetInt("FalseCount", FalseAnswerCount);
            }
        }


        if (QuestionList.Count > 0)
        {
            QuestionList.RemoveAt(0);
        }

        StartCoroutine(ChangeAnswersImages());
    }


    IEnumerator ChangeAnswersImages()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (QuestionList.Count != 0)
        {
            Answera.CopyPropertiesFromMaterial(correctionmat);
            Answerb.CopyPropertiesFromMaterial(correctionmat);
            Answerc.CopyPropertiesFromMaterial(correctionmat);
            Answera.mainTexture = QuestionList[0].AnswersMaterials[0].mainTexture;
            Answerb.mainTexture = QuestionList[0].AnswersMaterials[1].mainTexture;
            Answerc.mainTexture = QuestionList[0].AnswersMaterials[2].mainTexture;
        }
        else
        {
            Answera.CopyPropertiesFromMaterial(abackground);
            Answerb.CopyPropertiesFromMaterial(bbackground);
            Answerc.CopyPropertiesFromMaterial(cbackground);
            Answera.shader = abackground.shader;
            Answerb.shader = bbackground.shader;
            Answerc.shader = cbackground.shader;
        }
        EventManager.Instance.AnswerChanged();
    }



    public void ChangeCurrentAnswer(int value)
    {
        if (currentAnswerid + value < 0)
        {
            currentAnswerid = 2;
        }
        else if (currentAnswerid + value > 2)
        {
            currentAnswerid = 0;
        }
        else
        {
            currentAnswerid = currentAnswerid + value;
        }

    }


}
