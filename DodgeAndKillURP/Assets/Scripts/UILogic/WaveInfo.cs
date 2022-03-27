using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveInfo : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;
    [SerializeField] GameObject panel;
    int time = 0;

    public void StartCountDown(int time)
    {
        panel.SetActive(true);
        this.time = time;
        StartCoroutine(Counter());
    }

    private IEnumerator Counter()
    {
        for (int i = time-1; i > 0; i--)
        {
            tmpText.text = $"Next wave in {i}";
            yield return new WaitForSecondsRealtime(1f);
        }
        panel.SetActive(false);

        yield break;
    }
}
