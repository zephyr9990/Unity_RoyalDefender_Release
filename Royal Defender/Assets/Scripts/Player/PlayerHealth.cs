using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;   
    public GameObject gameOverPanel;
    public Slider healthSlider;   

    Animator anim;           
    AudioSource playerAudio;    
    PlayerMovement playerMovement; 
    PlayerCombat playerCombat;     
    PlayerInventory playerInventory;

    bool isDead;                   

    private int currentHealth;      

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerInventory = GetComponent < PlayerInventory >();

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateHealthBarUI();
            Die();
        }

        UpdateHealthBarUI();
    }

    private void UpdateHealthBarUI()
    {
        healthSlider.value = currentHealth;
    }

    public bool IsGreaterThanZero()
    {
        return currentHealth > 0;
    }


    public void Die()
    {
        isDead = true;
        anim.SetBool("IsDead", true);

        // Remove player controls
        playerMovement.enabled = false;
        playerCombat.enabled = false;
        playerInventory.EnableInventoryControl(false);
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}
