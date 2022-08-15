using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountdownCont : MonoBehaviour
{
    public int displayint;
    GameObject txt_Countdown;
    public Color diplayintcolor;
    public AudioClip beepsound;
    private void OnTriggerEnter(Collider other)
    {
        txt_Countdown = GameObject.Find("Txt_Countdown");
        if (other.tag == "Player")
        {
            if (displayint==0)
            {
                txt_Countdown.GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                txt_Countdown.GetComponent<TextMeshProUGUI>().text = displayint.ToString();
            }
            txt_Countdown.GetComponent<Animator>().Play("Txt_Countdownanim");
            StartCoroutine(CountdownFinishRoutine());
            txt_Countdown.GetComponent<TextMeshProUGUI>().color = diplayintcolor;
            GetComponent<AudioSource>().PlayOneShot(beepsound);
        }

    }

    IEnumerator CountdownFinishRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        txt_Countdown.GetComponent<TextMeshProUGUI>().text = "";
    }

}
