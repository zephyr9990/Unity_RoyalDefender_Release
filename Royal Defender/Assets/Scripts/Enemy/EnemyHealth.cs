﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth;
    public int pointsValue = 10;
    private EnemiesSlainManager enemiesSlainScript;
    private PointsManager pointsManagerScript;
    private Animator _anim;
    private bool bIsAlive = true;
    private int currentHealth;
    private NavMeshAgent _nav;

    public bool isLocked = false;
    public bool isDamaged = false;
    public int displayTime = 3;
    





    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        _anim = GetComponent<Animator>();
        _nav = GetComponent<NavMeshAgent>();
        enemiesSlainScript = GameObject.FindGameObjectWithTag("SlainText").GetComponent<EnemiesSlainManager>();
        pointsManagerScript = GameObject.FindGameObjectWithTag("PointText").GetComponent<PointsManager>();
    }

    public event Action<float> OnHealthPctChanged = delegate { };

    public void UpdateHealthPCT()
    {
        float currentHealthPct = (float)currentHealth / (float)MaxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    public void TakeDamage(int amount)
    {
        if (bIsAlive)
        {
            if (currentHealth - amount <= 0)
            {
                currentHealth = 0;
                isLocked = false;
                isDamaged = false;
                Die();
            }
            else
            {
                if (!isLocked)
                {
                    isDamaged = true;
                    StartCoroutine(TimedHealthBar(displayTime));
                }
                currentHealth -= amount;

                //Update the health slider;
                UpdateHealthPCT();
            }
            Debug.LogWarning(gameObject.name + " HP: " + currentHealth);

        }
    }

    IEnumerator TimedHealthBar(int time)
    {
        yield return new WaitForSeconds(time);
        isDamaged = false;
    }

    public bool IsAlive()
    {
        return bIsAlive;
    }

    private void Die()
    {
        _anim.SetTrigger("Die");

        if (_nav.enabled == true)
        _nav.isStopped = true;

        bIsAlive = false;
        enemiesSlainScript.CounterIncrease();
        pointsManagerScript.IncreasePoints(pointsValue);
        EnemyAIController enemyController = GetComponent<EnemyAIController>();
        if (enemyController)
        {
            //enemyController.DropWeapon();
        }
        else // flying enemy
        {
           // GetComponent<FlyingEnemyController>().DropWeapon();
        }
        Destroy(gameObject, 2.5f);
        
    }

    
}
