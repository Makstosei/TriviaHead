using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimTrigger : MonoBehaviour
{
    AnswerManager answerManager;
    bool istriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&& !istriggered)
        {
            istriggered = true;
            if (FindObjectOfType<AnswerManager>().isAnswerCorrect)
            {
                EventManager.Instance.isAnswerTrue(true);
            }
            else
            {
                EventManager.Instance.isAnswerTrue(false);
            }
        }
    }



}
