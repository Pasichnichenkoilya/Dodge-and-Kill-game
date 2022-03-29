using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle toggle;
    public TMP_Dropdown difficultyDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        InitResolutionDropdown();

        var data = SaveSystem.LoadPlayerSettings();

        toggle.isOn = data.fullscreen;

        SetFullscreen(data.fullscreen);
        SetResolution(data.resolutionIndex);
    }

    public void SetFullscreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;

        var data = SaveSystem.LoadPlayerSettings();
        data.fullscreen = isfullscreen;
        SaveSystem.SavePlayerSettings(data);

        Debug.Log($"Fullscreen set {isfullscreen}");
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);

        var data = SaveSystem.LoadPlayerSettings();
        data.resolutionIndex = resolutionIndex;
        SaveSystem.SavePlayerSettings(data);

        Debug.Log($"Resolution set {resolutionIndex}");
    }

    public void SetDifficulty(int index)
    {
        GameManager.Instance.difficulty = index + 1;
    }

    void InitResolutionDropdown()
    {
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add($"{resolutions[i].width} x {resolutions[i].height}");

            //if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            //{
            //    currentResolutionIndex = i;
            //}
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        var data = SaveSystem.LoadPlayerSettings();
        currentResolutionIndex = data.resolutionIndex;

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

}
