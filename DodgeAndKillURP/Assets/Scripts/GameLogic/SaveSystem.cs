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

        using (FileStream fs = new FileStream($"{Application.persistentDataPath}/{fileName}", FileMode.Create))
        {
            if (playerProgress == null)
                playerProgress = new PlayerProgress(GameManager.Instance.Inventory.Money, GameManager.Instance.PerkManager.activePerk.perkName, "testWeaponName");

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
            }
        }

        return data;
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
            }
        }

        return data;
    }

}

[System.Serializable]
public class PlayerSettings
{
    public bool fullscreen = false;
    public int resolutionIndex = 0;
}

[System.Serializable]
public class PlayerProgress
{
    public int money;
    public string perkName;
    public string weaponName;

    public PlayerProgress(int money, string perkName, string weaponName)
    {
        this.money = money;
        this.perkName = perkName;
        this.weaponName = weaponName;
    }
}