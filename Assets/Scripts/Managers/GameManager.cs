using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }
    #endregion

    public int LevelScore;
    public int LevelNo;
    public int TotalScore;
    public int isVibrationOn;
    public int isMusicon;
    public int isSoundOn;
    public int isCubeBuyed, isTriangleBuyed;
    bool click; // Next level Button Double Click Control

    private void OnEnable()
    {
        EventManager.onEndGameEvent += EndGameEvent;
    }

    private void OnDisable()
    {
        EventManager.onEndGameEvent -= EndGameEvent;

    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Data"))
        {
            if (PlayerPrefs.GetInt("Data") == 1)
            {

            }
            else
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("Data", 1);
            }
        }
        else
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Data", 1);
        }
        click = false;
        LevelCheck();
        ScoreCheck();
        SoundCheck();
        VibrationCheck();
        MusicCheck();    
    }


    public void StartGame()
    {
        EventManager.Instance.GameStart();
    }

    void EndGameEvent()
    {
        LevelNo++;
        PlayerPrefs.SetInt("LevelNo", LevelNo);
        TotalScore = TotalScore + FindObjectOfType<ScoreController>().levelscore;
        PlayerPrefs.SetInt("TotalScore", TotalScore);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        if (!click)
        {      
            click = true;
            StartCoroutine(GoNextlevel(0.5f, 0));
        }
    }

    public void FirstStartButton()
    {
        if (!click)
        {
            click = true;
            StartCoroutine(GoNextlevel(0.5f, 0));
        }
    }

    IEnumerator GoNextlevel(float waitTime, int level)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(level);

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void RestartLevelEndScreen()
    {
        LevelNo--;
        PlayerPrefs.SetInt("LevelNo", LevelNo);
        TotalScore = TotalScore - FindObjectOfType<ScoreController>().levelscore;
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }



    public void QuitGame()
    {
        Application.Quit();
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(0);
    }



    void LevelCheck()
    {
        if (PlayerPrefs.HasKey("LevelNo"))
        {
            if (PlayerPrefs.GetInt("LevelNo") == 0)
            {
                LevelNo = 1;
                PlayerPrefs.SetInt("LevelNo", LevelNo);
            }
            else
            {
                LevelNo = PlayerPrefs.GetInt("LevelNo");
            }
        }
        else
        {
            LevelNo = 1;
            PlayerPrefs.SetInt("LevelNo", LevelNo);
        }
    }
    void ScoreCheck()
    {
        if (PlayerPrefs.HasKey("TotalScore"))
        {
            TotalScore = PlayerPrefs.GetInt("TotalScore");
        }
        else
        {
            TotalScore = 0;
            PlayerPrefs.SetInt("TotalScore", TotalScore);
        }
    }
    void SoundCheck()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") > 0)
            {
                isSoundOn = 1;
                PlayerPrefs.SetInt("Sound", 1);
            }
            else
            {
                isSoundOn = 0;
                PlayerPrefs.SetInt("Sound", 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            isSoundOn = 1;
        }
    }
    void VibrationCheck()
    {
        if (PlayerPrefs.HasKey("Vibration"))
        {
            if (PlayerPrefs.GetInt("Vibration") > 0)
            {
                PlayerPrefs.SetInt("Vibration", 1);
                isVibrationOn = 1;
            }
            else
            {
                PlayerPrefs.SetInt("Vibration", 0);
                isVibrationOn = 0;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Vibration", 1);
            isVibrationOn = 1;
        }
    }

    void MusicCheck()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            if (PlayerPrefs.GetInt("Music") > 0)
            {
                PlayerPrefs.SetInt("Music", 1);
                isMusicon = 1;
            }
            else
            {
                PlayerPrefs.SetInt("Music", 0);
                isMusicon = 0;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
            isMusicon = 1;
        }
    }
}



