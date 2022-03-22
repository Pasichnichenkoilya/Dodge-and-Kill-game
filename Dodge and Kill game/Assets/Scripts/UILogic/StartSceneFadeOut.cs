using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartSceneFadeOut : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;

    void Start()
    {
        PauseMenu.Instance.Pause(false);
        StartCoroutine(Counter());
    }

    private IEnumerator Counter()
    {
        for (int i = 3; i > 0; i--)
        {
            tmpText.text = i.ToString();
            yield return new WaitForSecondsRealtime(.7f);
        }
        PauseMenu.Instance.Resume();
        gameObject.SetActive(false);
    }
}
