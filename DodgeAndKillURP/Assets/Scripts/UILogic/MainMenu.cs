using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        var data = SaveSystem.LoadPlayerSettings();
        var resolution = Screen.resolutions[data.resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, data.fullscreen);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
