using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public int startingHealth = 100;
    public int pointsAwardedOnDeath = 20;
    public float timeCanvasDisplayedFromDamage = 3f;
    private int currentHealth;
    private Animator animator;
    private EnemyUI enemyUI;
    private IAIController aiController;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        enemyUI = GetComponent<EnemyUI>();
        aiController = GetComponent<IAIController>();
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0)
        {
            return; // Already dead. Do nothing.
        }

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        float percentHealth = GetHealthPercentage();
        enemyUI.ShowCanvas(true, timeCanvasDisplayedFromDamage);
        enemyUI.SetHealthFillAmount(percentHealth);
    }

    public void RestoreHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
    }

    private float GetHealthPercentage()
    {
        return (float)currentHealth / startingHealth;
    }

    public bool IsGreaterThanZero()
    {
        return currentHealth > 0;
    }

    private void Die()
    {
        PointsManager.GetInstance().AddPoints(pointsAwardedOnDeath);
        animator.SetTrigger("Die");
        aiController.StopMovement();

        Destroy(gameObject, 3f);
    }
}