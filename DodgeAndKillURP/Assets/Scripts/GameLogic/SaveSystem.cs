using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayerProgress(PlayerProgress playerProgress = null, string fileName = "PlayerProgress.dat")
    {
        BinaryFormatter formatter = new BinaryFormatter();
        bool autosave = playerProgress == null;

        if (autosave)
        {
            playerProgress = LoadPlayerProgress();
            playerProgress.money = GameManager.Instance.Inventory.Money;
        }

        using (FileStream fs = new FileStream($"{Application.persistentDataPath}/{fileName}", FileMode.Create))
        {
            formatter.Serialize(fs, playerProgress);
        }
    }

    public static PlayerProgress LoadPlayerProgress(string fileName = "PlayerProgress.dat")
    {
        string path = $"{Application.persistentDataPath}/{fileName}";
        PlayerProgress data = null;
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                data = binaryFormatter.Deserialize(fileStream) as PlayerProgress;
                return data;
            }
        }

        return new PlayerProgress();
    }

    public static void SavePlayerSettings(PlayerSettings playerSettings, string fileName = "PlayerSettings.dat")
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = new FileStream($"{Application.persistentDataPath}/{fileName}", FileMode.Create))
        {
            formatter.Serialize(fs, playerSettings);
        }
    }

    public static PlayerSettings LoadPlayerSettings(string fileName = "PlayerSettings.dat")
    {
        string path = $"{Application.persistentDataPath}/{fileName}";
        PlayerSettings data = null;
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                data = binaryFormatter.Deserialize(fileStream) as PlayerSettings;
                return data;
            }
        }

        return new PlayerSettings();
    }
}

[System.Serializable]
public class PlayerSettings
{
    public bool fullscreen;
    public int resolutionIndex;

    public PlayerSettings() // default values
    {
        var resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                resolutionIndex = i;
                break;
            }
        }

        fullscreen = false;
    }
}

[System.Serializable]
public class PlayerProgress
{
    public int money;
    public string perkName;
    public string weaponName;
    public PooledObjectTag bulletType;

    public PlayerProgress() // default values
    {
        money = 0;
        perkName = "Dash";
        weaponName = "UR_17";
        bulletType = PooledObjectTag.DefaultBullet;
    }
}