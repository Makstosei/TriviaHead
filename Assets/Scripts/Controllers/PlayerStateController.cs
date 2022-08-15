using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public enum PlayerStates { Idle, Run, Dead, LockedControl, Fainted, FinishLine, Market }
    public PlayerStates playerState;
    public bool isGameEnded;
    AnswerManager answerManager;

    #region Singleton

    private static PlayerStateController _instance;

    public static PlayerStateController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerStateController>();
            return _instance;
        }
    }
    #endregion

    private void OnEnable()
    {
        EventManager.onGameStart += GameStarted;
        EventManager.onisLockedControl += isLockedControl;
        EventManager.onEndGameEvent += onEndGame;
        EventManager.onHitWall += HitWallEvent;
        EventManager.onFinishLine += FinishLine;
        EventManager.onLostLevel += LostLevel;
    }


    private void OnDisable()
    {
        EventManager.onGameStart -= GameStarted;
        EventManager.onisLockedControl -= isLockedControl;
        EventManager.onEndGameEvent -= onEndGame;
        EventManager.onHitWall -= HitWallEvent;
        EventManager.onFinishLine -= FinishLine;
        EventManager.onLostLevel -= LostLevel;
    }

    private void Awake()
    {
        if (!isGameEnded)
        {
            playerState = PlayerStates.Market;
        }
    }

    private void Start()
    {
        answerManager = FindObjectOfType<AnswerManager>();
    }


    void GameStarted()
    {
        if (!isGameEnded)
        {
            playerState = PlayerStates.Run;
        }


    }
    void isLockedControl(bool isLocked)
    {
        if (!isGameEnded)
        {
            if (isLocked)
            {
                playerState = PlayerStates.LockedControl;
            }
        }
    }

    void HitWallEvent(bool isFinishWall)
    {

        if (!isGameEnded && !isFinishWall)
        {
            if (!answerManager.isAnswerCorrect)
            {
                playerState = PlayerStates.Fainted;
            }
            else
            {
                playerState = PlayerStates.Run;
            }
        }
    }

    void LostLevel()
    {
        StartCoroutine(LostLevelRoutine());
    }
    
    IEnumerator LostLevelRoutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        playerState = PlayerStates.Dead;
        isGameEnded = true;
    }


    void onEndGame()
    {
        if (!isGameEnded)
        {
            playerState = PlayerStates.Idle;
        }

    }



    void FinishLine()
    {
        if (!isGameEnded)
        {
            playerState = PlayerStates.FinishLine;
        }

    }
}


