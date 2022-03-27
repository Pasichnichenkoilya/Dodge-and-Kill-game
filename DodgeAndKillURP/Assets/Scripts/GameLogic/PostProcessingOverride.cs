using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingOverride : MonoBehaviour
{
    [SerializeField] Volume volume;

    [SerializeField] float baseVignetteIntencity = .3f;
    float currentVignetteIntencity = .2f;
    [SerializeField] float goalVignetteIntencity = .3f;

    [SerializeField] float baseVignetteSmoothness = .5f;
    float currentVignetteSmoothness = .5f;
    [SerializeField] float goalVignetteSmoothness = 1f;

    [SerializeField] float baseChromaticAberration = .1f;
    float currentChromaticAberration = .1f;
    [SerializeField] float goalChromaticAberration = .3f;

    [SerializeField] float applyTime = .5f;
    [SerializeField] float revertTime = .3f;

    bool isApplied = false;

    private void Update()
    {
        Vignette v;
        volume.profile.TryGet<Vignette>(out v);
        v.intensity.Override(currentVignetteIntencity);
        v.smoothness.Override(currentVignetteSmoothness);

        ChromaticAberration ca;
        volume.profile.TryGet<ChromaticAberration>(out ca);
        ca.intensity.Override(currentChromaticAberration);
    }
    public void Apply()
    {
        StopAllCoroutines();

        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentVignetteIntencity = res, baseVignetteIntencity, goalVignetteIntencity, applyTime));
        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentVignetteSmoothness = res, baseVignetteSmoothness, goalVignetteSmoothness, applyTime));
        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentChromaticAberration = res, baseChromaticAberration, goalChromaticAberration, applyTime));
        isApplied = true;
    }

    public void Revert()
    {
        StopAllCoroutines();

        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentVignetteIntencity = res, currentVignetteIntencity, baseVignetteIntencity, revertTime));
        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentVignetteSmoothness = res, currentVignetteSmoothness, baseVignetteSmoothness, revertTime));
        StartCoroutine(GameManager.ChangeValueSmoothly(res => currentChromaticAberration = res, currentChromaticAberration, baseChromaticAberration, revertTime));
        isApplied = false;
    }
}
