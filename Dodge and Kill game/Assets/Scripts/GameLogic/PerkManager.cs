using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    [SerializeField] Player player;
    Perk activePerk;

    public List<Perk> perks = new List<Perk>();

    private void Start()
    {
        //activePerk = LoadPerk();
        SetActivePerk(1);
    }

    public void SetActivePerk(int index)
    {
        foreach (var p in perks)
        {
            p.gameObject.SetActive(false);
        }
        activePerk = perks[index];
        activePerk.gameObject.SetActive(true);
        player.perk = activePerk;
    }
}
