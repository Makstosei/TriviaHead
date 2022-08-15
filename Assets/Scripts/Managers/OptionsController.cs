using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public GameObject Sound, Vibration, Music;
    public Sprite Soundon, SoundOff, Musicon, Musicoff, Vibrationon, Vibrationoff;
    int issoundon, ismusicon, isvibrationon;



    private void Start()
    {
        issoundon = GameManager.Instance.isSoundOn;
        ismusicon = GameManager.Instance.isMusicon;
        isvibrationon = GameManager.Instance.isVibrationOn;
        MusicCheck();
        VibrationCheck();
        SoundCheck();
    }


    public void SoundButton()
    {
        if (issoundon == 1)
        {
            issoundon = 0;
            GameManager.Instance.isSoundOn = 0;
            PlayerPrefs.SetInt("Sound", 0);
            Sound.GetComponent<Image>().sprite = SoundOff;
        }
        else
        {
            issoundon = 1;
            GameManager.Instance.isSoundOn = 1;
            PlayerPrefs.SetInt("Sound", 1);
            Sound.GetComponent<Image>().sprite = Soundon;
        }
    }
    public void VibrationButton()
    {
        if (isvibrationon == 1)
        {
            isvibrationon = 0;
            GameManager.Instance.isVibrationOn = 0;
            PlayerPrefs.SetInt("Vibration", 0);
            Vibration.GetComponent<Image>().sprite = Vibrationoff;
        }
        else
        {
            GameManager.Instance.isVibrationOn = 1;
            PlayerPrefs.SetInt("Vibration", 1);
            Vibration.GetComponent<Image>().sprite = Vibrationon;
            isvibrationon = 1;
        }
    }
    public void MusicButton()
    {
        if (ismusicon == 1)
        {
            ismusicon = 0;
            GameManager.Instance.isMusicon = 0;
            PlayerPrefs.SetInt("Music", 0);
            Music.GetComponent<Image>().sprite = Musicoff;
        }
        else
        {
            ismusicon = 1;
            GameManager.Instance.isMusicon = 1;
            PlayerPrefs.SetInt("Music", 1);
            Music.GetComponent<Image>().sprite = Musicon;
        }
    }
    void MusicCheck()
    {
        if (ismusicon == 1)
        {
            Music.GetComponent<Image>().sprite = Musicon;
        }
        else
        {
            Music.GetComponent<Image>().sprite = Musicoff;
        }
    }
    void SoundCheck()
    {
        if (issoundon == 1)
        {
            Sound.GetComponent<Image>().sprite = Soundon;
        }
        else
        {
            Sound.GetComponent<Image>().sprite = SoundOff;
        }
    }
    void VibrationCheck()
    {
        if (isvibrationon == 1)
        {
            Vibration.GetComponent<Image>().sprite = Vibrationon;
        }
        else
        {
            Vibration.GetComponent<Image>().sprite = Vibrationoff;
        }
    }

}
