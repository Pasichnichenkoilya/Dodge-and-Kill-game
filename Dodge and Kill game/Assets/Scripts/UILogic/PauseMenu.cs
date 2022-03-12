using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; }

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Animator animator;

    private void Start()
    {
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
        GameManager.Instance.PauseManager.SetPaused(IsGamePaused);
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        IsGamePaused = false;
        GameManager.Instance.PauseManager.SetPaused(IsGamePaused);
    }

    public void MainMenu()
    {
        animator.SetTrigger("MainMenuLoad");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameManager.Instance.PauseManager.SetPaused(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
