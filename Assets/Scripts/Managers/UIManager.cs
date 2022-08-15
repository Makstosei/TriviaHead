using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject StartGameUI;
    [SerializeField] GameObject InGameUI;
    [SerializeField] GameObject Pause_UI;
    [SerializeField] GameObject LostLevel_UI;
    [SerializeField] GameObject EndGame_UI;
    [SerializeField] GameObject Market_UI;
    [SerializeField] GameObject ConnectionError_UI;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI txtlevelstart, txtlevelingame, txtlevellost, txtlevelend,txtlevelmarket;


    public bool ifGameStarted;
    public int levelno;
    public int LevelScore, TotalScore;
    public float templevelscore;
    public void OnEnable()
    {
        EventManager.onGameStart += StartUI;
        EventManager.onGamePausedEvent += GamePausedEvent;
        EventManager.onEndGameEvent += EndGameUI;
        EventManager.onContinueGameEvent += ContinueGameEvent;
        EventManager.onLostLevel += LostLevelEvent;
        EventManager.onConnectionError += ConnectionErrorEvent;
        EventManager.onMarketClosed += MarketClosedEvent;

    }
    public void OnDisable()
    {
        EventManager.onGameStart -= StartUI;
        EventManager.onGamePausedEvent -= GamePausedEvent;
        EventManager.onEndGameEvent -= EndGameUI;
        EventManager.onContinueGameEvent -= ContinueGameEvent;
        EventManager.onLostLevel -= LostLevelEvent;
        EventManager.onConnectionError -= ConnectionErrorEvent;
        EventManager.onMarketClosed -= MarketClosedEvent;

    }

    private void Start()
    {
        levelno = GameManager.Instance.LevelNo;
        if (SceneManager.GetActiveScene().buildIndex.Equals(1))
        {
            txtlevelstart.text = "Level " + levelno.ToString();
            txtlevelingame.text = "Level " + levelno.ToString();
            txtlevellost.text = "Level " + levelno.ToString();
            txtlevelend.text = "Level " + levelno.ToString();
            txtlevelmarket.text = "Level " + levelno.ToString();
        }
    }

    void ConnectionErrorEvent(string ConnectionError)
    {
        StartGameUI.SetActive(false);
        ConnectionError_UI.SetActive(true);
    }

    void MarketClosedEvent()
    {
        Market_UI.SetActive(false);
        EventManager.Instance.GameStart();
    }

    void StartUI()
    {
        StartGameUI.SetActive(false);
        InGameUI.SetActive(true);
    }
    void GamePausedEvent(bool isGameStarted)
    {
        Time.timeScale = 0;
        ifGameStarted = isGameStarted;
        InGameUI.SetActive(false);
        Pause_UI.SetActive(true);
        StartGameUI.SetActive(false);
    }

    void ContinueGameEvent()
    {
        Time.timeScale = 1;
        if (ifGameStarted)
        {
            InGameUI.SetActive(true);
            Pause_UI.SetActive(false);
        }
        else
        {

            StartGameUI.SetActive(true);
            Pause_UI.SetActive(false);
        }
    }

    void LostLevelEvent()
    {
        InGameUI.SetActive(false);
        StartCoroutine(DelayedLostScreen());
    }

    IEnumerator DelayedLostScreen()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        LostLevel_UI.SetActive(true);
    }


    void EndGameUI()
    {
        levelno = GameManager.Instance.LevelNo;
        TotalScore = GameManager.Instance.TotalScore;
        EndGame_UI.SetActive(true);
        InGameUI.SetActive(false);
    }


    public void PauseButtonatStart()
    {
        EventManager.Instance.GamePausedEvent(false);
    }

    public void PauseButtonatInGame()
    {
        EventManager.Instance.GamePausedEvent(true);
    }

    public void ContinueGame()
    {
        EventManager.Instance.ContinueGameEvent();
    }



}
