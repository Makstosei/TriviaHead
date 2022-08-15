using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int Levelno;
    public int TotalCoin;
    public bool isVibrateOn;
    public bool isSoundOn;
    public int totaltrue;
    public int totalfalse;

    public PlayerData(PlayerData playerData)
    {
        Levelno = playerData.Levelno;
        TotalCoin = playerData.TotalCoin;
        isVibrateOn = playerData.isVibrateOn;
        isSoundOn = playerData.isSoundOn;
        totalfalse = playerData.totalfalse;
        totaltrue = playerData.totaltrue;
    }
}
