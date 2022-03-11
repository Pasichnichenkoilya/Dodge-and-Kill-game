using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; }

    public static event EventHandler OnPause;
    public static event EventHandler OnResume;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Animator animator;

    float oldTimeScale;

    private void Start()
    {
        oldTimeScale = Time.timeScale;
        Resume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        IsGamePaused = true;
        OnPause?.Invoke(this, EventArgs.Empty);

        pauseMenuUI.SetActive(true);
        oldTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = oldTimeScale;
        IsGamePaused = false;
        OnResume?.Invoke(this, EventArgs.Empty);
    }

    public void MainMenu()
    {
        animator.SetTrigger("MainMenuLoad");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
