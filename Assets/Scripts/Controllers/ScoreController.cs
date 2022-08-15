using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int levelscore;

    public void OnEnable()
    {
        EventManager.onScoreUpdate += ScoreUpdateEvent;
        EventManager.onLostLevel += onLostLevel;
    }
    public void OnDisable()
    {
        EventManager.onScoreUpdate -= ScoreUpdateEvent;
        EventManager.onLostLevel -= onLostLevel;
    }


    void ScoreUpdateEvent()
    {
        if (FindObjectOfType<ComboBarController>().comboint == 0 || FindObjectOfType<ComboBarController>() == null)
        {
            levelscore += 1 * 5;
        }
        else
        {
            levelscore += FindObjectOfType<ComboBarController>().comboint * 10;
        }
    }

    void onLostLevel()
    {
        levelscore = 0;
    }



}
