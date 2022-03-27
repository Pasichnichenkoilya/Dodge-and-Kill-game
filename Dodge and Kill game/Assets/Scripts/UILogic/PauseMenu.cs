using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    public static bool IsGamePaused { get; private set; }
    public bool checkForInput;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Animator animator;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }

    private void Start()
    {
        IsGamePaused = false;
        checkForInput = true;
        GameManager.Instance.PauseManager.SetPaused(IsGamePaused);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && checkForInput)
        {
            if (IsGamePaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause(bool showUI = true)
    {
        IsGamePaused = true;
        GameManager.Instance.PauseManager.SetPaused(IsGamePaused);
        pauseMenuUI.SetActive(showUI);
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
        GameManager.Instance.PauseManager.SetPaused(false);
        checkForInput = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
