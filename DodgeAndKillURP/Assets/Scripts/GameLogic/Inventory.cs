using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private int money;

    private Weapon currentWeapon;

    List<Weapon> weapons;
    Dictionary<GameObject, Perk> perks;

    public Weapon CurrentWeapon { get => currentWeapon; private set => currentWeapon = value; }
    public int Money { get => money; set => money = value; }

    public Inventory()
    {

        weapons = new List<Weapon>();
        perks = new Dictionary<GameObject, Perk>();

        Money = 0;

        //LoadInventory(); // load inventory from db
    }

    public void AddMoney(int amount)
    {
        this.Money += amount;
    }

    public bool TryRemoveMoney(int amount)
    {
        if (money - amount < 0)
            return false;

        money -= amount;
        return true;
    }

    public bool HasWeapon(string weaponName)
    {
        return weapons.FirstOrDefault(w => w.name == weaponName) != null;
    }

    public void EquipWeapon(string weaponName)
    {
        if (!HasWeapon(weaponName))
            return;

        CurrentWeapon = weapons.FirstOrDefault(w => w.name == weaponName);
    }

    public void AddWeapon(Weapon weapon)
    {
        if (HasWeapon(weapon.weaponName))
            return;

        weapons.Add(weapon);
    }

    private void LoadInventory()
    {
        throw new NotImplementedException();
    }
}
