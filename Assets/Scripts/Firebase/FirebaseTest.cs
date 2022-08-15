using Firebase;
using UnityEngine;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Firebase.Extensions;
using UnityEngine.UI;

public class FirebaseTest : MonoBehaviour
{
    DatabaseReference databaseRef;
    public DataSnapshot QuestionsData;
    public QuestionsData qData;
    public Material FirebaseRefMat;
    int Successcount;
    public Slider progressBar; //Gösterge
    int levelno, improvedlevelno;

    private void Awake()
    {
        qData = GameObject.Find("QuestionDatabase").GetComponent<QuestionsData>();
        StartCoroutine(StartingRoutine());
        progressBar = GameObject.Find("Slider").GetComponent<Slider>();
    }


    IEnumerator StartingRoutine()//firebase database baðlantý
    {
        yield return new WaitForSecondsRealtime(1f);
        var task = FirebaseApp.CheckAndFixDependenciesAsync();
        while (!task.IsCompleted)
        {
            yield return null;
        }
        if (task.IsCanceled || task.IsFaulted)
        {
            EventManager.Instance.ConnectionError("Senkron olamadý.");
        }
        var dependencyStatus = task.Result;
        if (dependencyStatus == DependencyStatus.Available)
        {
            databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
            yield return new WaitForSecondsRealtime(0.5f);
            StartCoroutine(GetDataSnapshot());
        }
        else
        {
            EventManager.Instance.ConnectionError("Can not reach to Firebase Database");
        }
    }

    public IEnumerator GetDataSnapshot()//snapshot alma ve database kopyalama
    {
        yield return new WaitForSecondsRealtime(0.5f);
        FirebaseDatabase.DefaultInstance.RootReference.Child("Questions").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                EventManager.Instance.ConnectionError("Failed Snapshot");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                QuestionsData = snapshot;
                qData.QdataQuestionList = new List<QuestionsData.Questions>();
                GetQuestionFromDatabase();
            }
        });
    }

    void GetQuestionFromDatabase()
    {
        levelno = GameManager.Instance.LevelNo;

        //buradaki  "7" leveldeki soru sayýsýdýr.
        //databasedeki soru sayýsýnýn leveldeki soru sayýna bölümünden kalaný alýp,
        //database toplam soru sayýsýndan cýkarýyoruz ve gelen deðeri tekrar toplam soru sayýna bölerek kaç level yeteceðini buluyoruz.
        //daha sonra 12/18 gibi lvl'lerde 1. seviyede çekmesi gereken sorularý özel olarak belirmemiz gerekiyor diðer levellerde
        //level no'ya göre bölümün kalaný direk geliyor.
        if (levelno > 0 && levelno <= ((int)QuestionsData.ChildrenCount - (int)QuestionsData.ChildrenCount % 7) / 7)
        {
            improvedlevelno = levelno;
        }
        else if (levelno % (((int)QuestionsData.ChildrenCount - (int)QuestionsData.ChildrenCount % 7) / 7) == 0 || levelno == 0)
        {
            improvedlevelno = (((int)QuestionsData.ChildrenCount - (int)QuestionsData.ChildrenCount % 7) / 7);
        }
        else
        {
            improvedlevelno = levelno % (((int)QuestionsData.ChildrenCount - (int)QuestionsData.ChildrenCount % 7) / 7);
        }


        for (int i = improvedlevelno * 7 - 6; i < improvedlevelno * 7 + 1; i++)
        {
            string jsonstring = QuestionsData.Child(i.ToString()).GetRawJsonValue();
            QuestionsData.Questions question1 = (QuestionsData.Questions)JsonUtility.FromJson(jsonstring, typeof(QuestionsData.Questions));
            qData.QdataQuestionList.Add(question1);
            qData.QdataQuestionList[qData.QdataQuestionList.Count - 1].fontName = "Question" + qData.QdataQuestionList.Count.ToString();
            Material material1 = new Material(FirebaseRefMat.shader);
            material1.CopyPropertiesFromMaterial(FirebaseRefMat);
            Material material2 = new Material(FirebaseRefMat.shader);
            material2.CopyPropertiesFromMaterial(FirebaseRefMat);
            Material material3 = new Material(FirebaseRefMat.shader);
            material3.CopyPropertiesFromMaterial(FirebaseRefMat);
            StartCoroutine(ImageCoRoutine(question1.Answer1, material1));
            StartCoroutine(ImageCoRoutine(question1.Answer2, material2));
            StartCoroutine(ImageCoRoutine(question1.Answer3, material3));
            question1.AnswersMaterials.Add(material1);
            question1.AnswersMaterials.Add(material2);
            question1.AnswersMaterials.Add(material3);
        }
    }

    IEnumerator QuestionsSyncronizedRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        EventManager.Instance.QuestionsSyncronized();
    }

    IEnumerator ImageCoRoutine(string url, Material answer)
    {
        using (UnityWebRequest UWR = UnityWebRequestTexture.GetTexture(url))
        {
            yield return UWR.SendWebRequest();
            while (UWR.result == UnityWebRequest.Result.InProgress)
            {
                Debug.Log("uwr while inprogress");
            }
            if (UWR.result == UnityWebRequest.Result.ConnectionError || UWR.result == UnityWebRequest.Result.ProtocolError || UWR.result == UnityWebRequest.Result.DataProcessingError)
            {
                EventManager.Instance.ConnectionError("Some error: while trying to reach Answers");
            }
            else if (UWR.result == UnityWebRequest.Result.Success)
            {
                Successcount++;
                answer.mainTexture = DownloadHandlerTexture.GetContent(UWR);
                progressBar.value = qData.QdataQuestionList.Count / (int)QuestionsData.ChildrenCount * 0.5f + (Successcount / ((int)QuestionsData.ChildrenCount * 3) * 0.3f);
                if (Successcount == qData.QdataQuestionList.Count * 3)
                {
                    StartCoroutine(QuestionsSyncronizedRoutine());
                }

            }
        }
    }

}