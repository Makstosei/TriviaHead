using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollapseAudio : MonoBehaviour
{
    public List<AudioClip> collapseSounds;
    
    private void OnEnable()
    {
        EventManager.onHitWall += hitWall;
    }
    private void OnDisable()
    {
        EventManager.onHitWall -= hitWall;
    }

    void hitWall(bool isTrue)
    {
        if (PlayerPrefs.GetInt("Sound") >= 1)
        {
            int random = Random.Range(0, collapseSounds.Count);
            GetComponent<AudioSource>().PlayOneShot(collapseSounds[random]);
        }
    }
}

