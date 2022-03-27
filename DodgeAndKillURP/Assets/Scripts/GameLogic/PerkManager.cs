using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerkManager : MonoBehaviour
{
    [SerializeField] Player player;
    public Perk activePerk;

    public List<Perk> perks = new List<Perk>();

    private void Start()
    {
        //activePerk = LoadPerk(perkName);
        //SetActivePerk(1);

        //SaveSystem.SavePlayerProgress();
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

    public void SetActivePerk(string perkName)
    {
        foreach (var p in perks)
        {
            if (p.perkName == perkName)
            {
                activePerk = p;
                activePerk.gameObject.SetActive(true);
                player.perk = activePerk;
            }
            else
            {
                p.gameObject.SetActive(false);
            }
        }
        
    }
}
