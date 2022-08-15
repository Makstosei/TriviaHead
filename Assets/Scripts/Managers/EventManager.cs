using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    #region Singleton

    private static EventManager _instance;

    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<EventManager>();
            return _instance;
        }
    }
    #endregion

    public static Action onQuestionSyncronized;
    public static Action onGameStart;
    public static Action<bool> onGamePausedEvent;
    public static Action onContinueGameEvent;
    public static Action onEndGameEvent;
    public static Action onLostLevel;
    public static Action onScoreUpdate;
    public static Action onFinishLine;
    public static Action<bool> onHitWall;
    public static Action<bool> onisLockedControl;
    public static Action<bool> onisAnswerTrue;
    public static Action<int> onHealthChanged;
    public static Action<string> onConnectionError;
    public static Action onAnswerChanged;
    public static Action onMarketClosed;


    public void GameStart()
    {
        onGameStart.Invoke();
    }

    public void GamePausedEvent(bool isGameStarted)
    {
        onGamePausedEvent.Invoke(isGameStarted);
    }

    public void ContinueGameEvent()
    {
        onContinueGameEvent.Invoke();
    }

    public void LostLevel()
    {
        onLostLevel.Invoke();
    }

    public void AnswerChanged()
    {
        onAnswerChanged.Invoke();
    }

    public void EndGameEvent()
    {
        onEndGameEvent.Invoke();
    }

    public void ScoreUpdate()
    {
        onScoreUpdate.Invoke();
    }
    public void HitWall(bool isFinishWall)
    {
        onHitWall.Invoke(isFinishWall);
    }

    public void QuestionsSyncronized()
    {
        onQuestionSyncronized.Invoke();
    }

    public void isAnswerTrue(bool isTrue)
    {
        onisAnswerTrue(isTrue);
    }

    public void isLockedControl(bool isLocked)
    {
        onisLockedControl(isLocked);
    }

    public void FinishLine()
    {
        onFinishLine.Invoke();
    }

    public void HealthChanged(int CurrentHealth)
    {
        onHealthChanged.Invoke(CurrentHealth);
    }

    public void MarketClosed()
    {
        onMarketClosed.Invoke();
    }

    public void ConnectionError(string ErrorMessage)
    {
        onConnectionError.Invoke(ErrorMessage);
        Debug.Log(ErrorMessage);
    }

}
