using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject playerHand;

    public Weapon activeWeapon;

    public List<Weapon> weapons = new List<Weapon>();

    private void Start()
    {
        //var data = SaveSystem.LoadPlayerProgress();
        ////if (data != null)
        ////{
        ////    SetBullet(data.weaponName, data.bulletType);
        ////    SetActiveWeapon(data.weaponName);
        ////    return;
        ////}
        //data.money = 0;
        //data.weaponName = weapons[0].weaponName;
        //data.perkName = "SlowMotion";
        //data.bulletType = PooledObjectTag.DefaultBullet;

        //SetActiveWeapon(weapons[0].weaponName);
        //SetBullet(weapons[0].weaponName, PooledObjectTag.ToxicBullet);
    }

    public void SetBullet(string weaponName, PooledObjectTag bullet)
    {
        foreach (var w in weapons)
        {
            if (w.weaponName == weaponName && w is FirearmWeapon fw)
            {
                fw.objectTag = bullet;
            }
        }
    }

    public void SetActiveWeapon(string weaponName)
    {
        foreach (var w in weapons)
        {
            if (w.weaponName == weaponName)
            {
                activeWeapon = w;
                activeWeapon.gameObject.SetActive(true);
                activeWeapon.gameObject.transform.SetParent(playerHand.transform, false);
                player.weapon = activeWeapon;
            }
            else
            {
                w.gameObject.SetActive(false);
            }
        }

    }
}
