using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPositionRotator : MonoBehaviour
{
    AnswerManager answerManager;
    public GameObject head1, head2, head3, head1pos, head2pos, head3pos, HeadAnswers, HeadBonePos;
    public bool leftrotate, rightrotate, rotating, gameEnded;
    public Component Trail1, Trail2, Trail3;
    public GameObject Ping1, Ping2, Ping3;
    bool playerrotate;
    float targetz;

    private void OnEnable()
    {
        EventManager.onHitWall += Event_HitWall;
        EventManager.onFinishLine += Event_FinishLine;
        EventManager.onLostLevel += Event_EndLevel;
        EventManager.onEndGameEvent += Event_EndLevel;
    }

    private void OnDisable()
    {
        EventManager.onHitWall -= Event_HitWall;
        EventManager.onFinishLine -= Event_FinishLine;
        EventManager.onLostLevel -= Event_EndLevel;
        EventManager.onEndGameEvent -= Event_EndLevel;
    }

    void Event_EndLevel()
    {
        gameEnded = true;
        HeadAnswers.GetComponent<Animator>().Play("New State");
        GetComponent<Animator>().Play("New State");
    }

    void Event_FinishLine()
    {
        switch (answerManager.currentAnswerid)
        {
            case 0:
                head1.gameObject.SetActive(true);
                head2.gameObject.SetActive(false);
                head3.gameObject.SetActive(false);
                Ping1.SetActive(false);
                Ping2.SetActive(false);
                Ping3.SetActive(false);
                break;
            case 1:
                head1.gameObject.SetActive(false);
                head2.gameObject.SetActive(true);
                head3.gameObject.SetActive(false);
                Ping1.SetActive(false);
                Ping2.SetActive(false);
                Ping3.SetActive(false);
                break;
            case 2:
                head1.gameObject.SetActive(false);
                head2.gameObject.SetActive(false);
                head3.gameObject.SetActive(true);
                Ping1.SetActive(false);
                Ping2.SetActive(false);
                Ping3.SetActive(false);
                break;
            default:
                break;

        }
    }

    void Event_HitWall(bool isFinishWall)
    {
        if (!isFinishWall&& !gameEnded)
        {
            StartCoroutine(HitwallResetMatAnimPlay());
            
        }
        else
        {

        }
    }


    IEnumerator HitwallResetMatAnimPlay()
    {
        rotating = true;
        head1.gameObject.transform.position = head1pos.gameObject.transform.position;
        head2.gameObject.transform.position = head2pos.gameObject.transform.position;
        head3.gameObject.transform.position = head3pos.gameObject.transform.position;
        HeadAnswers.GetComponent<Animator>().Play("HeadAnswers");
        GetComponent<Animator>().Play("RotateAbleAnswers");
        yield return new WaitForSecondsRealtime(1.5f);
        rotating = false;
    }


    private void Start()
    {
        answerManager = FindObjectOfType<AnswerManager>();
        HeadBonePos = GameObject.Find("HeadBonePos");
    }

    private void Update()
    {
        UpdateHeadPos();
    }





    public void LeftRotate()
    {
        if (!rotating)
        {
            rotating = true;
            playerrotate = true;
            switch (answerManager.currentAnswerid)
            {
                case 0:
                    targetz = 120;
                    break;
                case 1:
                    targetz = 360;
                    break;
                case 2:
                    targetz = 240;
                    break;
            }
            StartCoroutine(Rotate(0.5f, -1));
        }

    }

    public void RightRotate()
    {
        if (!rotating)
        {
            rotating = true;
            playerrotate = true;
            switch (answerManager.currentAnswerid)
            {
                case 0:
                    targetz = -120;
                    break;
                case 1:
                    targetz = 120;
                    break;
                case 2:
                    targetz = 0;
                    break;
            }
            StartCoroutine(Rotate(0.5f, 1));
        }

    }

    public IEnumerator Rotate(float duration, int value)
    {
        if (FindObjectOfType<PlayerSwipeSound>() != null && GameManager.Instance.isSoundOn == 1)
        {
            FindObjectOfType<PlayerSwipeSound>().AnswerChanged();
        }
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, targetz, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, yRotation);
            yield return null;
        }
        answerManager.ChangeCurrentAnswer(value);
        EventManager.Instance.AnswerChanged();
        rotating = false;
        playerrotate = false;
    }


    void UpdateHeadPos()
    {
        if (!gameEnded)
        {
            if (rotating)
            {
                head1.gameObject.transform.position = head1pos.gameObject.transform.position;
                head2.gameObject.transform.position = head2pos.gameObject.transform.position;
                head3.gameObject.transform.position = head3pos.gameObject.transform.position;
                if (playerrotate)
                {
                    head1.GetComponent<TrailRenderer>().enabled = true;
                    head2.GetComponent<TrailRenderer>().enabled = true;
                    head3.GetComponent<TrailRenderer>().enabled = true;
                }

            }

            else if (!rotating)
            {
                if (!playerrotate)
                {
                    head1.GetComponent<TrailRenderer>().enabled = false;
                    head2.GetComponent<TrailRenderer>().enabled = false;
                    head3.GetComponent<TrailRenderer>().enabled = false;
                }

                switch (answerManager.currentAnswerid)
                {
                    case 0:
                        head1.gameObject.transform.position = HeadBonePos.gameObject.transform.position;
                        head2.gameObject.transform.position = head2pos.gameObject.transform.position;
                        head3.gameObject.transform.position = head3pos.gameObject.transform.position;
                        Ping1.SetActive(true);
                        Ping2.SetActive(false);
                        Ping3.SetActive(false);
                        break;
                    case 1:
                        head1.gameObject.transform.position = head1pos.gameObject.transform.position;
                        head2.gameObject.transform.position = HeadBonePos.gameObject.transform.position;
                        head3.gameObject.transform.position = head3pos.gameObject.transform.position;
                        Ping1.SetActive(false);
                        Ping2.SetActive(true);
                        Ping3.SetActive(false);
                        break;
                    case 2:
                        head1.gameObject.transform.position = head1pos.gameObject.transform.position;
                        head2.gameObject.transform.position = head2pos.gameObject.transform.position;
                        head3.gameObject.transform.position = HeadBonePos.gameObject.transform.position;
                        Ping1.SetActive(false);
                        Ping2.SetActive(false);
                        Ping3.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }
        else if (gameEnded)
        {
            StopCoroutine(HitwallResetMatAnimPlay());
            switch (answerManager.currentAnswerid)
            {
                case 0:
                    head1.gameObject.transform.position = HeadBonePos.gameObject.transform.position;
                    head2.gameObject.SetActive(false);
                    head3.gameObject.SetActive(false);
                    Ping1.SetActive(false);
                    Ping2.SetActive(false);
                    Ping3.SetActive(false);
                    break;
                case 1:
                    head1.gameObject.SetActive(false);
                    head2.gameObject.transform.position = HeadBonePos.gameObject.transform.position;
                    head3.gameObject.SetActive(false);
                    Ping1.SetActive(false);
                    Ping2.SetActive(false);
                    Ping3.SetActive(false);
                    break;
                case 2:
                    head1.gameObject.SetActive(false);
                    head2.gameObject.SetActive(false);
                    head3.gameObject.transform.position = HeadBonePos.gameObject.transform.position;
                    Ping1.SetActive(false);
                    Ping2.SetActive(false);
                    Ping3.SetActive(false);
                    break;
                default:
                    break;
            }
        }

    }


}
