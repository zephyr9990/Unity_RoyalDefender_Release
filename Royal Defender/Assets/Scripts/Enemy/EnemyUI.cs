using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour, ICanvas
{
    public Canvas enemyCanvas;
    public Slider healthSlider;
    public Text healthText;

    private bool lockedOnto;

    private void Awake()
    {
        lockedOnto = false;
    }

    // Update is called once per frame
    private void Update()
    {
        enemyCanvas.transform.rotation = Camera.main.transform.rotation;
    }

    private void SetHealthFillAmount(float hpPercent)
    {
        healthSlider.value = hpPercent;
    }

    public void IsLockedOn(bool lockedOn)
    {
        lockedOnto = lockedOn;
        ShowCanvas(lockedOn);
    }

    public void ShowCanvas(bool isShowing)
    {
        if (IsDead())
        {
            return; // keep canvas visible if dead.
        }

        EnableCanvas(isShowing);
    }

    public void ShowCanvas(bool isShowing, float time)
    {
        if (IsDead())
        {
            return; // keep canvas visible if dead.
        }

        EnableCanvas(isShowing);


        // Only disable canvas if not locked onto
        if (!lockedOnto)
        {
            Invoke("DisableCanvas", time);
        }
    }

    public void SetHealthUIValues(int currentHealth, int maxHealth, float percentHealth)
    {
        SetHealthText(currentHealth, maxHealth);
        SetHealthFillAmount(percentHealth);
    }

    private void SetHealthText(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth + "/" + maxHealth;
    }

    private void EnableCanvas(bool isActive)
    {
        enemyCanvas.gameObject.SetActive(isActive);
    }

    private void DisableCanvas()
    {
        enemyCanvas.gameObject.SetActive(false);
    }

    private bool IsDead()
    {
        return healthSlider.value <= 0;
    }
}
