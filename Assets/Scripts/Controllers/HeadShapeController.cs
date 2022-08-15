using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShapeController : MonoBehaviour
{
    public GameObject H1sphere, H1cube, H1triangle, H2sphere, H2cube, H2triangle, H3sphere, H3cube, H3triangle;
    AnswerManager answerManager;
    int headType;

    private void Awake()
    {
        answerManager = FindObjectOfType<AnswerManager>();

    }
    private void OnEnable()
    {
        EventManager.onMarketClosed += MarketClosedEvent;
    }
    private void OnDisable()
    {
        EventManager.onMarketClosed -= MarketClosedEvent;
    }



    void MarketClosedEvent()
    {
        headType = PlayerPrefs.GetInt("HeadType");
        switch (headType)
        {
            case 0:
                H1cube.SetActive(false);
                H1sphere.SetActive(true);
                H1triangle.SetActive(false);
                H2cube.SetActive(false);
                H2sphere.SetActive(true);
                H2triangle.SetActive(false);
                H3cube.SetActive(false);
                H3sphere.SetActive(true);
                H3triangle.SetActive(false);
                break;
            case 1:
                H1cube.SetActive(true);
                H1sphere.SetActive(false);
                H1triangle.SetActive(false);
                H2cube.SetActive(true);
                H2sphere.SetActive(false);
                H2triangle.SetActive(false);
                H3cube.SetActive(true);
                H3sphere.SetActive(false);
                H3triangle.SetActive(false);

                break;
            case 2:
                H1cube.SetActive(false);
                H1sphere.SetActive(false);
                H1triangle.SetActive(true);
                H2cube.SetActive(false);
                H2sphere.SetActive(false);
                H2triangle.SetActive(true);
                H3cube.SetActive(false);
                H3sphere.SetActive(false);
                H3triangle.SetActive(true);
                break;
            default:
                break;



        }

    }

}
