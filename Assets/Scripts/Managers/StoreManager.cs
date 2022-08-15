using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StoreManager : MonoBehaviour
{
    public Sprite starticon, Buyicon,CantBuyicon;
    public Image BuyButonIcon;
    public TextMeshProUGUI BuyButtonText,totalScoreText;
    public Button buyButton;
    AnswerManager answerManager;
    public int CubeBuyPrice = 300;
    public int TriangleBuyPrice = 350;
    public int currentAnswerid;
    int headType;
    int isTriangleBuyed, isCubeBuyed;
    int totalScore;

    private void OnEnable()
    {
        EventManager.onAnswerChanged += AnswerChangedEvent;
    }
    private void OnDisable()
    {
        EventManager.onAnswerChanged -= AnswerChangedEvent;
    }
    void AnswerChangedEvent()
    {
        BuyButtonIconUpdate();
    }

    private void Awake()
    {
        answerManager = FindObjectOfType<AnswerManager>();
    }

    private void Start()
    {
        totalScore = GameManager.Instance.TotalScore;
        totalScoreText.text = totalScore.ToString();
        PlayerRefCheck();
        BuyButtonIconUpdate();
    }

    public void BuyButtonIconUpdate()
    {
        currentAnswerid = answerManager.currentAnswerid;

        switch (answerManager.currentAnswerid)
        {
            case 0:
                BuyButtonText.text = "";
                BuyButonIcon.sprite = starticon;
                buyButton.interactable = true;

                break;
            case 1:
                if (isCubeBuyed == 1)
                {
                    BuyButtonText.text = "";
                    BuyButonIcon.sprite = starticon;
                    buyButton.interactable = true;
                }
                else
                {
                    if (totalScore<CubeBuyPrice)
                    {
                        buyButton.interactable = false;
                    }
                    else
                    {
                        buyButton.interactable = true;

                    }
                    BuyButtonText.text = CubeBuyPrice.ToString();
                    BuyButonIcon.sprite = Buyicon;
                }
                break;
            case 2:
                if (isTriangleBuyed == 1)
                {
                    BuyButtonText.text = "";
                    BuyButonIcon.sprite = starticon;
                    buyButton.interactable = true;
                }
                else
                {
                    if (totalScore < TriangleBuyPrice)
                    {
                        buyButton.interactable = false;
                    }
                    else
                    {
                        buyButton.interactable = true;

                    }
                    BuyButtonText.text = TriangleBuyPrice.ToString();
                    BuyButonIcon.sprite = Buyicon;
                }
                break;
            default:
                break;



        }
    }


    public void BuyHead()
    {
        switch (answerManager.currentAnswerid)
        {
            case 0:
                headType = 0;
                PlayerPrefs.SetInt("HeadType", headType);
                EventManager.Instance.MarketClosed();
                break;
            case 1:
                if (isCubeBuyed == 0)
                {
                    totalScore -= CubeBuyPrice;
                    GameManager.Instance.TotalScore = totalScore;
                    PlayerPrefs.SetInt("TotalScore", isCubeBuyed);
                    isCubeBuyed = 1;
                    PlayerPrefs.SetInt("isCubeBuyed", isCubeBuyed);
                    totalScoreText.text = totalScore.ToString();
                    BuyButtonIconUpdate();
                }
                else
                {
                    headType = 1;
                    PlayerPrefs.SetInt("HeadType", headType);
                    EventManager.Instance.MarketClosed();
                }
                break;
            case 2:
                if (isTriangleBuyed == 0)
                {
                    totalScore -= TriangleBuyPrice;
                    GameManager.Instance.TotalScore = totalScore;
                    PlayerPrefs.SetInt("TotalScore", isCubeBuyed);
                    isTriangleBuyed = 1;
                    PlayerPrefs.SetInt("isTriangleBuyed", isTriangleBuyed);
                    totalScoreText.text = totalScore.ToString();
                    BuyButtonIconUpdate();

                }
                else
                {
                    headType = 2;
                    PlayerPrefs.SetInt("HeadType", headType);
                    EventManager.Instance.MarketClosed();
                }

                break;
            default:
                break;



        }

    }

    void PlayerRefCheck()
    {
        if (PlayerPrefs.HasKey("isCubeBuyed"))
        {
            if (PlayerPrefs.GetInt("isCubeBuyed") >= 1)
            {
                isCubeBuyed = 1;
                PlayerPrefs.SetInt("isCubeBuyed", 1);
            }
            else
            {
                isCubeBuyed = 0;
                PlayerPrefs.SetInt("isCubeBuyed", isCubeBuyed);
            }
        }
        else
        {
            isCubeBuyed = 0;
            PlayerPrefs.SetInt("isCubeBuyed", isCubeBuyed);
        }

        if (PlayerPrefs.HasKey("isTriangleBuyed"))
        {
            if (PlayerPrefs.GetInt("isTriangleBuyed") >= 1)
            {
                isTriangleBuyed = 1;
                PlayerPrefs.SetInt("isTriangleBuyed", 1);
            }
            else
            {
                isTriangleBuyed = 0;
                PlayerPrefs.SetInt("isTriangleBuyed", isTriangleBuyed);
            }
        }
        else
        {
            isTriangleBuyed = 0;
            PlayerPrefs.SetInt("isTriangleBuyed", isTriangleBuyed);
        }
    }

}
