using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyHealth : MonoBehaviour, IHealthWithWaveManager
{
    public int startingHealth = 100;
    public int pointsAwardedOnDeath = 20;
    public float timeCanvasDisplayedFromDamage = 3f;
    private int currentHealth;
    private Animator animator;
    private EnemyUI enemyUI;
    private Loot loot;
    private WaveManager waveManager;
    private IAIController aiController;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        enemyUI = GetComponent<EnemyUI>();
        loot = GetComponent<Loot>();
        aiController = GetComponent<IAIController>();
    }

    private void Start()
    {
        float percentHealth = GetHealthPercentage();
        enemyUI.SetHealthUIValues(currentHealth, startingHealth, percentHealth);
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
        enemyUI.SetHealthUIValues(currentHealth, startingHealth, percentHealth);
    }

    public void RestoreHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }
    }

    public void SetWaveManager(WaveManager waveManager)
    {
        this.waveManager = waveManager;
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
        loot.DropItem();
        waveManager.DecreaseEnemyCount();

        Destroy(gameObject, 3f);
    }
}