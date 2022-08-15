using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataManager : MonoBehaviour
{
   public PlayerData playerData;
 
    private void OnEnable()
    {
        //EventManager.onisAnswerTrue += isAnswerTrue;
        //EventManager.onEndGameEvent += onEndGame;
    }
    private void OnDisable()
    {
        //EventManager.onisAnswerTrue -= isAnswerTrue;
        //EventManager.onEndGameEvent -= onEndGame;

    }

    
    void onEndGame()
    {
        SaveLoadSystem.SavePlayer(playerData);
    }

    void isAnswerTrue(bool isTrue)
    {
        if (isTrue)
        {
            playerData.totaltrue++;
        }
        else
        {
            playerData.totalfalse++;

        }
    }



}
