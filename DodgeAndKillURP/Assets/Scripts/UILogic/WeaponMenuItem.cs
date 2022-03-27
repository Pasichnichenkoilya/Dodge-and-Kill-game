using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponMenuItem : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text buttonText;
    [SerializeField] bool isBought;
    [SerializeField] bool isEquipped;

    private void Start()
    {
        weaponName.text = weapon.name.Replace('_', ' ');
    }

    public void SetBought(bool isBought)
    {
        this.isBought = isBought;
        buttonText.text = isBought ? "EQUIP" : "BUY";
    }

    public void SetEquip(bool isEquipped)
    {
        this.isBought = isEquipped;
        buttonText.text = isEquipped ? "EQUIPPED" : "EQUIPPED";
    }
}
