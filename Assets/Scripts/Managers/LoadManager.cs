using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadManager : MonoBehaviour
{

    public Slider progressBar; //Gösterge
    public GameObject gameManager;

   
    private void OnEnable()
    {
        EventManager.onQuestionSyncronized += QuestionSyncronized;
 
    }

    private void OnDisable()
    {
        EventManager.onQuestionSyncronized -= QuestionSyncronized;

    }

    void QuestionSyncronized()
    {
        StartCoroutine(startLoading(1));
    }


    IEnumerator startLoading(int level)
    {
        yield return new WaitForSecondsRealtime(1.5f);
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        while (!async.isDone)
        {
            progressBar.value =0.8f+ async.progress;
            yield return null;
        }

    }



}