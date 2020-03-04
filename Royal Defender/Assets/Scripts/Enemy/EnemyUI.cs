using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour, ICanvas
{
    public Canvas enemyCanvas;
    public Image HealthFillImage;

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

    public void SetHealthFillAmount(float hpPercent)
    {
        HealthFillImage.fillAmount = hpPercent;
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
        return HealthFillImage.fillAmount <= 0;
    }
}
