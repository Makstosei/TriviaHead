using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreEndUICounterAnim : MonoBehaviour
{
    private bool GameEnded, stopsound;
    public float Templevelscore, levelscore;
    [SerializeField] TextMeshProUGUI ScoreText;
    EndGameScoreBarAudioController scorebarAudioController;

    private void OnEnable()
    {
        EventManager.onEndGameEvent += EndGameEvent;
    }
    private void OnDisable()
    {
        EventManager.onEndGameEvent -= EndGameEvent;

    }



    void Update()
    {
        if (GameEnded)
        {
            if (Templevelscore + 0.5 < levelscore)
            {
                Templevelscore = Mathf.Lerp(Templevelscore, levelscore, 3 * Time.deltaTime);
                ScoreText.text = Mathf.RoundToInt(Templevelscore).ToString();
                if (Mathf.RoundToInt(Templevelscore) % 5 == 0 && stopsound != true)
                {
                    scorebarAudioController.PlayClickSound();
                }               
            }
            else
            {
                Templevelscore = levelscore;
                ScoreText.text = Mathf.RoundToInt(Templevelscore).ToString();
                stopsound = true;
                GameEnded = false;
                scorebarAudioController.PlayClickSound();
            }
        }

    }

    void EndGameEvent()
    {
        levelscore = FindObjectOfType<ScoreController>().levelscore;
        scorebarAudioController = FindObjectOfType<EndGameScoreBarAudioController>();
        GameEnded = true;
    }


}
