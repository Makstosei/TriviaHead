using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoadingScreenTips : MonoBehaviour
{
    public List<string> Tips;
    int randomtipinteger;
    bool canSlide;
    public GameObject TipsText1,TipsText2;
    bool tip2front;

    void Start()
    {
        RandomTip();
        StartCoroutine(TipsSlideRoutine());
    }

    void Update()
    {
        if (canSlide)
        {
            TipsText1.transform.position = new Vector3(TipsText1.transform.position.x - 50 * Time.deltaTime, TipsText1.transform.position.y, TipsText1.transform.position.z);
            TipsText2.transform.position = new Vector3(TipsText2.transform.position.x - 50 * Time.deltaTime, TipsText1.transform.position.y, TipsText2.transform.position.z);

            if (tip2front)
            {
                if (TipsText2.transform.localPosition.x <= -2160)
                {
                    TipsText2.transform.localPosition = new Vector3(0, TipsText2.transform.localPosition.y, TipsText2.transform.localPosition.z);
                    tip2front = false;
                }
            }
            else
            {             
                if (TipsText1.transform.localPosition.x <= -1080)
                {
                    TipsText1.transform.localPosition = new Vector3(1080, TipsText1.transform.localPosition.y, TipsText1.transform.localPosition.z);
                    tip2front = true;
                }
            }


        }
    }


    IEnumerator TipsSlideRoutine()
    {
        yield return new WaitForSecondsRealtime(2f);
        canSlide = true;
    }

    void RandomTip()
    {
        randomtipinteger = Random.Range(0, Tips.Count);
        TipsText1.GetComponent<TextMeshProUGUI>().text = Tips[randomtipinteger];
        TipsText2.GetComponent<TextMeshProUGUI>().text = Tips[randomtipinteger];
    }

}
