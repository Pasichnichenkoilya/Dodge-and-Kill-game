using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PerkMenu : MonoBehaviour
{
    [SerializeField] TMP_Text selectedPerkText;
    [SerializeField] TMP_Text moneyText;

    private void Start()
    {
        var data = SaveSystem.LoadPlayerProgress();
        moneyText.text = $"Money: {data.money}";

        SetPerk(data.perkName);
    }

    public void SetPerk(string perkName)
    {
        var data = SaveSystem.LoadPlayerProgress();
        data.perkName = perkName;

        SaveSystem.SavePlayerProgress(data);
        selectedPerkText.text = $"Selected perk: {perkName}";
    }
}
