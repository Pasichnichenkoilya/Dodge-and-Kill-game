using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponsMenu : MonoBehaviour
{
    [SerializeField] TMP_Text selectedWeaponText;
    [SerializeField] TMP_Text selectedBulletText;
    [SerializeField] TMP_Text moneyText;

    private void Start()
    {
        var data = SaveSystem.LoadPlayerProgress();
        moneyText.text = $"Money: {data.money}";

        SetWeapon(data.weaponName);
        SetBullet((int)data.bulletType);
    }

    public void SetWeapon(string weaponName)
    {
        var data = SaveSystem.LoadPlayerProgress();

        if (data == null)
        {
            data = new PlayerProgress();
        }

        data.weaponName = weaponName;

        SaveSystem.SavePlayerProgress(data);
        selectedWeaponText.text = $"Selected weapon: {weaponName}";
    }

    public void SetBullet(int bullet)
    {
        var data = SaveSystem.LoadPlayerProgress();
        if (data == null)
        {
            data = new PlayerProgress();
        }

        data.bulletType = ((PooledObjectTag)bullet);
        SaveSystem.SavePlayerProgress(data);

        selectedBulletText.text = $"Selected bullet: {((PooledObjectTag)bullet)}";
    }
}