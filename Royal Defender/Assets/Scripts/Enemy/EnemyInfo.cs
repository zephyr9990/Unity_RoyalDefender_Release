using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public GameObject enemy;
    public int initialWaveAmount;
    private int currentAmount;
    private int roundDivisor;

    public EnemyInfo()
    {
        enemy = null;
        initialWaveAmount = 0;
        roundDivisor = 5;
        currentAmount = initialWaveAmount;
    }

    public void ResetWaveAmount(int waveNumber)
    {
        if (waveNumber / roundDivisor != 0)
        {
            initialWaveAmount += initialWaveAmount / 2;
            currentAmount = initialWaveAmount;
        }
        else
        {
            currentAmount = initialWaveAmount;
        }
    }

    public void DecrementAmount()
    {
        currentAmount--;
    }

    public int GetAmount()
    {
        return currentAmount;
    }
}
