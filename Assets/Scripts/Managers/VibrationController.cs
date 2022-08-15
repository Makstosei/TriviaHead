using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
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
        answerManager = FindObjectOfType<AnswerManager>();
    }


    void HitWallEvent(bool isFinishwall)
    {
        if (!isFinishwall && !answerManager.isAnswerCorrect)
        {
            if (GameManager.Instance.isVibrationOn >= 1)
            {
                NiceVibrationsManager.HeavyImpact();
            }
        }
    }




  

}
