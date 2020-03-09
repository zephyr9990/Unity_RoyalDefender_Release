using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isShowing;

    private void Awake()
    {
        isShowing = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        isShowing = !isShowing;
        pausePanel.SetActive(isShowing);

        if (isShowing)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        TogglePauseMenu();
    }

    public void Restart()
    {
        TogglePauseMenu();
        SceneManager.LoadScene("CastleTownScene");
    }

    public void ReturnToMainMenu()
    {
        TogglePauseMenu();
        SceneManager.LoadScene("MainMenu");
    }
}
