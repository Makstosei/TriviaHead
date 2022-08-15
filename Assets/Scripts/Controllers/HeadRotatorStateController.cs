using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotatorStateController : MonoBehaviour
{
    public enum HeadRotatorStates { Market,EndGame,Rotating,AnswerChange,Idle }
    public HeadRotatorStates headRotatorState;

    #region Singleton

    private static HeadRotatorStateController _instance;

    public static HeadRotatorStateController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<HeadRotatorStateController>();
            return _instance;
        }
    }
    #endregion

    private void OnEnable()
    {
        EventManager.onGameStart += GameStartedEvent;
        EventManager.onEndGameEvent += onEndGameEvent;
        EventManager.onFinishLine += FinishLineEvent;
        EventManager.onLostLevel += LostLevelEvent;
    }


    private void OnDisable()
    {
        EventManager.onGameStart -= GameStartedEvent;
        EventManager.onEndGameEvent -= onEndGameEvent;
        EventManager.onFinishLine += FinishLineEvent;
        EventManager.onLostLevel -= LostLevelEvent;
    }





    void GameStartedEvent()
    {
        headRotatorState = HeadRotatorStates.Market;
    }


    void onEndGameEvent()
    {
        headRotatorState = HeadRotatorStates.EndGame;
    }

    void LostLevelEvent()
    {
        headRotatorState = HeadRotatorStates.EndGame;
    }

    void FinishLineEvent()
    {

    }



}
