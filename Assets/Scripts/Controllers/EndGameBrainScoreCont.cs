using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameBrainScoreCont : MonoBehaviour
{
    public GameObject brain1, brain2, brain3;
    EndGameScoreBarAudioController scorebarAudioController;
    int levelscore;

    private void OnEnable()
    {
        EventManager.onEndGameEvent += EndGameEvent;
    }
    private void OnDisable()
    {
        EventManager.onEndGameEvent -= EndGameEvent;

    }


    void EndGameEvent()
    {
        StartCoroutine(ShowSprites());
        levelscore = FindObjectOfType<ScoreController>().levelscore;
        scorebarAudioController = FindObjectOfType<EndGameScoreBarAudioController>();

    }

    IEnumerator ShowSprites()
    {
        yield return new WaitForSecondsRealtime(1f);

        if (levelscore >= 350)
        {
            brain1.SetActive(true);
            scorebarAudioController.PlayBrainPopUp();
            yield return new WaitForSecondsRealtime(0.5f);
            brain2.SetActive(true);
            scorebarAudioController.PlayBrainPopUp();
            yield return new WaitForSecondsRealtime(0.5f);
            brain3.SetActive(true);
            scorebarAudioController.PlayBrainPopUp();
        }
        else if (levelscore < 350 && levelscore >= 190)
        {
            brain1.SetActive(true);
            scorebarAudioController.PlayBrainPopUp();
            yield return new WaitForSecondsRealtime(0.5f);
            brain2.SetActive(true);
            scorebarAudioController.PlayBrainPopUp();
            yield return new WaitForSecondsRealtime(0.5f);
            brain3.SetActive(false);
        }
        else if (levelscore < 220 && levelscore >= 80)
        {
            brain1.SetActive(true);
            scorebarAudioController.PlayBrainPopUp();
            yield return new WaitForSecondsRealtime(0.5f);
            brain2.SetActive(false);
            yield return new WaitForSecondsRealtime(0.5f);
            brain3.SetActive(false);
        }
        else
        {
            brain1.SetActive(false);
            brain2.SetActive(false);
            brain3.SetActive(false);
        }
    }


}
