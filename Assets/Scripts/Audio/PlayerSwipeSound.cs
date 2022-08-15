using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipeSound : MonoBehaviour
{
    public AudioClip Whip;


    public void AnswerChanged()
    {
        GetComponent<AudioSource>().PlayOneShot(Whip);
    }

}
