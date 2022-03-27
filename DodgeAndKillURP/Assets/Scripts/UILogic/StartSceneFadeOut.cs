using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartSceneFadeOut : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;
    [SerializeField] int countTime = 3;

    
    void Start()
    {
        GameManager.Instance.PauseManager.SetPaused(true, false);
        StartCoroutine(Counter());
    }

    private IEnumerator Counter()
    {
        for (int i = countTime; i > 0; i--)
        {
            tmpText.text = i.ToString();
            yield return new WaitForSecondsRealtime(.7f);
        }
        GameManager.Instance.PauseManager.SetPaused(false, false);
        gameObject.SetActive(false);

        yield break;
    }
}
