using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        // TODO Finish take damage logic
    }

    public void RestoreHealth(int restoreAmount)
    {
        // TODO Finish restore health logic.
    }

    void Death()
    {
        // TODO Finish death logic.
    }
}
