using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown difficultyDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        InitResolutionDropdown();
    }

    public void SetFullscreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }

    public void SetDifficulty(int index)
    {
        GameManager.Instance.difficulty = index + 1;
    }

    void InitResolutionDropdown()
    {
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = resolutions.Length - 1; i >= 0; i--)
        {
            options.Add($"{resolutions[i].width} x {resolutions[i].height}");

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = resolutions.Length - 1 - i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

}
