using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionsData :MonoBehaviour
{
    public List<Questions> QdataQuestionList = new List<Questions>();  

    [System.Serializable]
    public class Questions
    {
        public string fontName="";
        public string Question;
        public string Answer1;
        public string Answer2;
        public string Answer3;
        public int Correct;
        public string Category;
        public List<Material> AnswersMaterials = new List<Material>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
