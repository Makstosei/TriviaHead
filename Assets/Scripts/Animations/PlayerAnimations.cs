using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    GameObject player;
    HealthSystem healthSystem;
    bool isGameEnded;

    private void OnEnable()
    {
        EventManager.onGameStart += GameStarted;
        EventManager.onisAnswerTrue += isAnswerTrue;
        EventManager.onisLockedControl += isLockedControl;
        EventManager.onEndGameEvent += onEndGame;
        EventManager.onLostLevel += LostLevel;
        EventManager.onFinishLine += FinishLine;
    }

    private void OnDisable()
    {
        EventManager.onGameStart -= GameStarted;
        EventManager.onisAnswerTrue -= isAnswerTrue;
        EventManager.onisLockedControl -= isLockedControl;
        EventManager.onEndGameEvent -= onEndGame;
        EventManager.onLostLevel -= LostLevel;
        EventManager.onFinishLine -= FinishLine;

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        healthSystem = FindObjectOfType<HealthSystem>();
    }




    void isAnswerTrue(bool isTrue)
    {
        if (isTrue)
        {
            animator.SetBool("True", true);
            StartCoroutine(ResetTrueFalse());
        }
        else
        {
            if (healthSystem.CurrentHealth - 1 <= 0)
            {
                animator.SetBool("False", true);
                StartCoroutine(ResetTrueFalse());
            }
            else
            {
                animator.SetBool("False", true);
                StartCoroutine(ResetTrueFalse());
            }


        }
    }
    IEnumerator ResetTrueFalse()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        animator.SetBool("True", false);
        animator.SetBool("False", false);
    }

    void FinishLine()
    {
        animator.SetBool("WallRun", false);
        animator.SetBool("isRun", false);
        animator.SetBool("True", false);
        animator.SetBool("False", false);
        animator.SetBool("Victory", false);
        animator.SetBool("Fail", false);
        animator.SetBool("FinishRun", true);

    }

    void GameStarted()
    {
        animator.SetBool("isRun", true);
    }

    void isLockedControl(bool isLocked)
    {
        if (isLocked)
        {
            animator.SetBool("WallRun", true);
        }
        else
        {
            animator.SetBool("WallRun", false);
        }

    }
    void LostLevel()
    {
        isGameEnded = true;
        animator.SetBool("WallRun", false);
        animator.SetBool("isRun", false);
        animator.SetBool("True", false);
        animator.SetBool("False", true);
        animator.SetBool("Victory", false);
        animator.SetBool("Fail", true);
        animator.SetBool("FinishRun", false);

    }
    void onEndGame()
    {
        isGameEnded = true;
        animator.SetBool("WallRun", false);
        animator.SetBool("isRun", false);
        animator.SetBool("True", false);
        animator.SetBool("False", false);
        animator.SetBool("Victory", true);
        animator.SetBool("Fail", false);
        animator.SetBool("FinishRun", false);

    }



    void RunAnimTrigger()
    {
        if (!isGameEnded)
        {
            PlayerStateController.Instance.playerState = PlayerStateController.PlayerStates.Run;
        }
    }

  



}
