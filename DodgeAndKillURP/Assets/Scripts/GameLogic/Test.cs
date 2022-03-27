using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Test : MonoBehaviour
{
    [SerializeField] Volume volume;

    [SerializeField] float baseVignetteIntencity = .3f;
    [SerializeField] float currentVignetteIntencity = .2f;
    [SerializeField] float goalVignetteIntencity = .3f;

    [SerializeField] float baseVignetteSmoothness = .5f;
    [SerializeField] float currentVignetteSmoothness = .5f;
    [SerializeField] float goalVignetteSmoothness = 1f;

    [SerializeField] float baseChromaticAberration = .1f;
    [SerializeField] float currentChromaticAberration = .1f;
    [SerializeField] float goalChromaticAberration = .3f;

    [SerializeField] float applyTime = .5f;
    [SerializeField] float revertTime = .3f;

    bool isApplied = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (isApplied)
            {
                Revert();
            }
            else
            {
                Apply();
            }
        }

        Vignette v;
        volume.profile.TryGet<Vignette>(out v);
        v.intensity.Override(currentVignetteIntencity);
        v.smoothness.Override(currentVignetteSmoothness);

        ChromaticAberration ca;
        volume.profile.TryGet<ChromaticAberration>(out ca);
        ca.intensity.Override(currentChromaticAberration);

    }
    private void Apply()
    {
        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentVignetteIntencity = res, baseVignetteIntencity, goalVignetteIntencity, applyTime));
        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentVignetteSmoothness = res, baseVignetteSmoothness, goalVignetteSmoothness, applyTime));
        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentChromaticAberration = res, baseChromaticAberration, goalChromaticAberration, applyTime));
        isApplied = true;
    }

    private void Revert()
    {
        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentVignetteIntencity = res, goalVignetteIntencity, baseVignetteIntencity, revertTime));
        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentVignetteSmoothness = res, goalVignetteSmoothness, baseVignetteSmoothness, revertTime));
        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentChromaticAberration = res, goalChromaticAberration, baseChromaticAberration, revertTime));
        isApplied = false;
    }
}
