using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScoreBarAudioController : MonoBehaviour
{
    public AudioClip clicksound;
    public List<AudioClip> brainpopupSounds;
    private int brainpopupCounter;

    public void PlayClickSound()
    {
        if (GameManager.Instance.isSoundOn==1)
        {
            GetComponent<AudioSource>().volume = 0.5f;
            GetComponent<AudioSource>().PlayOneShot(clicksound);
        }
    }
    public void PlayBrainPopUp()
    {
        if (GameManager.Instance.isSoundOn == 1)
        {
            GetComponent<AudioSource>().volume = 1f;
            GetComponent<AudioSource>().PlayOneShot(brainpopupSounds[brainpopupCounter]);
            brainpopupCounter++;
        }
    }


}
