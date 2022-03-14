using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : Perk, IPauseHandler
{
    [Range(1f, .1f)]
    [SerializeField] float slowDownFactor;
    [SerializeField] float startSlowMotionTime;
    [SerializeField] float endSlowMotionTime;
    float maxValue = 1;

    public ManaBar manaBar;
    bool readyForAction = true;
    bool isAction = false;
    float actionProgress;

    private void Start()
    {
        actionProgress = maxValue;
        GameManager.Instance.PauseManager.Subscribe(this);
    }

    private void PauseMenu_OnResume(object sender, EventArgs e)
    {
        StartCoroutine(SlowMotionCoolDown());
    }

    private void PauseMenu_OnPause(object sender, EventArgs e)
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        manaBar.SetValue(actionProgress);
    }

    public override void Action()
    {
        if (readyForAction)
        {
            StopAllCoroutines();
            StartCoroutine(DoSlowMotion());
        }
        else if (!readyForAction && isAction)
        {
            StopAllCoroutines();
            StartCoroutine(SlowMotionCoolDown());
        }
    }

    private IEnumerator DoSlowMotion()
    {
        readyForAction = false;
        isAction = true;

        /// smoothly slowing time to slowDownFactor
        StartCoroutine(GameManager.ChangeValueSmoothly(res => Time.timeScale = res, Time.timeScale, slowDownFactor, startSlowMotionTime));
        StartCoroutine(GameManager.ChangeValueSmoothly(res => Time.fixedDeltaTime = res, Time.fixedDeltaTime, Time.timeScale * .002f, startSlowMotionTime));
        yield return new WaitForSeconds(startSlowMotionTime * slowDownFactor);
        /// 

        StartCoroutine(GameManager.ChangeValueSmoothly(res => actionProgress = res, actionProgress, 0, duration * actionProgress, true));
        yield return new WaitForSeconds(duration * slowDownFactor * actionProgress);

        StartCoroutine(SlowMotionCoolDown());

        yield break;
    }

    private IEnumerator SlowMotionCoolDown()
    {
        while (PauseMenu.IsGamePaused)
            yield return null;

        var actualCoolDown = baseCoolDown - baseCoolDown * actionProgress;// calculating cooldown with how mutch mana left

        isAction = false;

        /// smoothly returning time to normal
        StartCoroutine(GameManager.ChangeValueSmoothly(res => Time.timeScale = res, Time.timeScale, 1, endSlowMotionTime, true));
        StartCoroutine(GameManager.ChangeValueSmoothly(res => Time.fixedDeltaTime = res, Time.fixedDeltaTime, Time.fixedDeltaTime = 0.02f * Time.timeScale, endSlowMotionTime, true));
        yield return new WaitForSeconds(startSlowMotionTime * slowDownFactor);
        /// 

        StartCoroutine(GameManager.ChangeValueSmoothly(
            result => actionProgress = result,
            actionProgress,
            maxValue,
            actualCoolDown));

        readyForAction = true;

        yield return new WaitForSeconds(actualCoolDown);

        yield break;
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
        {
            StopAllCoroutines();
        }
        else
        {
            StartCoroutine(SlowMotionCoolDown());
        }
    }
}
