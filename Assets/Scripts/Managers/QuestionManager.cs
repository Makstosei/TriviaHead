using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    QuestionsData qDataSO;
    public List<QuestionsData.Questions> QuestionList = new List<QuestionsData.Questions>();
    public int levelno;

    private void OnEnable()
    {
        EventManager.onGameStart += GetQuestions;
    }

    private void OnDisable()
    {
        EventManager.onGameStart -= GetQuestions;
    }

    private void Awake()
    {
        qDataSO = GameObject.Find("QuestionDatabase").GetComponent<QuestionsData>();
        levelno = GameManager.Instance.LevelNo;
    }



    public void GetQuestions()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            QuestionList.Add(qDataSO.QdataQuestionList[i]);
            gameObject.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = qDataSO.QdataQuestionList[i].Question.ToString();
        }
        EventManager.Instance.QuestionsSyncronized();
    }

}
