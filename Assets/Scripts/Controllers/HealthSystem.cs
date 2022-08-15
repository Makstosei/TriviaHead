using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth, CurrentHealth;

    private void OnEnable()
    {
        EventManager.onHitWall += HitWallEvent;
    }
    private void OnDisable()
    {
        EventManager.onHitWall -= HitWallEvent;
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }


    void HitWallEvent(bool isFinishWall)
    {
        if (!isFinishWall && !FindObjectOfType<AnswerManager>().isAnswerCorrect)
        {

            if (CurrentHealth - 1 <= 0)
            {
                CurrentHealth--;
                EventManager.Instance.LostLevel();
            }
            else
            {
                CurrentHealth--;
                EventManager.Instance.HealthChanged(CurrentHealth);
            }



        }

    }


}
