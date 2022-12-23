using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int day;
    public int balance;
    public int additionalRatio;

    public GameData()
    {
        day = 1;
        balance = 1000;
        additionalRatio = 0;
    }
}
