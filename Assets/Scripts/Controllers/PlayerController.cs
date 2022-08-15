using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    private bool stopTouch;
    private Vector3 firstPressPos;
    private Vector3 currentPos;
    public int swipeRange = 50;
    public int speed;
    private HeadPositionRotator RotateableHeadScript;
    bool levelended;

    private void OnEnable()
    {
        EventManager.onLostLevel += LevelEnded;
        EventManager.onEndGameEvent += LevelEnded;
    }

    private void OnDisable()
    {
        EventManager.onLostLevel -= LevelEnded;
        EventManager.onEndGameEvent -= LevelEnded;
    }
    private void Start()
    {
        RotateableHeadScript = GameObject.Find("RotateAbleAnswers").GetComponent<HeadPositionRotator>();
    }

    private void Update()
    {
        switch (PlayerStateController.Instance.playerState)
        {
            case PlayerStateController.PlayerStates.Idle:
                speed = 0;
                break;
            case PlayerStateController.PlayerStates.Run:
                speed = 2;
                CheckandRotate();
                transform.Translate(Vector3.forward * speed * Time.deltaTime);//run
                break;
            case PlayerStateController.PlayerStates.Dead:
                StopCoroutine(FaintedRoutine());
                break;
            case PlayerStateController.PlayerStates.LockedControl:
                speed = 3;
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                break;
            case PlayerStateController.PlayerStates.Fainted:
                speed = 1;
                StartCoroutine(FaintedRoutine());
                transform.Translate(Vector3.forward * speed * Time.deltaTime);//run
                break;
            case PlayerStateController.PlayerStates.FinishLine:
                speed = 3;
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                break;
            case PlayerStateController.PlayerStates.Market:
                speed = 0;
                break;
            default:
                break;
        }
    }

    private void CheckandRotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stopTouch = false;
            firstPressPos = Input.mousePosition;
        }
        //swipe check
        if (Input.GetMouseButton(0))
        {
            currentPos = Input.mousePosition;
            Vector2 Distance = firstPressPos - currentPos;
            if (!stopTouch)
            {
                if (Distance.x < -swipeRange)//right
                {
                    RotateableHeadScript.RightRotate();
                }
                else if (Distance.x > swipeRange)//left
                {
                    RotateableHeadScript.LeftRotate();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            stopTouch = true;
        }

    }

    void LevelEnded()
    {
        levelended = true;
    }

    IEnumerator FaintedRoutine()
    {
       
        yield return new WaitForSecondsRealtime(1f);
        if (!levelended)
        {
            PlayerStateController.Instance.playerState = PlayerStateController.PlayerStates.Run;
        }
    }


  
}








