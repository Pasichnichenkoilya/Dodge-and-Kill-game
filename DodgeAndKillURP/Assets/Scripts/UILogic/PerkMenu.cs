using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PerkMenu : MonoBehaviour
{
    [SerializeField] TMP_Text selectedText;

    private void Start()
    {
        var data = SaveSystem.LoadPlayerProgress();
        SetPerk(data.perkName);
    }

    public void SetPerk(string perkName)
    {
        var data = SaveSystem.LoadPlayerProgress();
        data.perkName = perkName;

        SaveSystem.SavePlayerProgress(data);
        selectedText.text = $"Selected: {perkName}";
    }
}
