using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Perk
{
    public ManaBar perkBar;
    [SerializeField] Player player;

    [SerializeField] float dashForce = 10f;
    [SerializeField] float maxDash = 1f;

    bool readyForDash = true;
    bool isDashing;
    float dash;

    public override void Action()
    {
        if (readyForDash && player.moveDelta.magnitude > 0)
        {
            StartCoroutine(Dashing());
        }
    }

    private void Start()
    {
        isDashing = false;
        dash = maxDash;
        perkBar.SetMaxValue(maxDash);
    }

    private void Update()
    {
        perkBar.SetValue(dash);
    }

    private IEnumerator Dashing()
    {
        readyForDash = false;
        player.speed += dashForce;
        isDashing = true;

        StartCoroutine(GameManager.ChangeValueSmoothly(res => dash = res, dash, 0, duration / 2));//change dash ui to 0 smoothly
        yield return new WaitForSeconds(duration);

        StartCoroutine(GameManager.ChangeValueSmoothly(res => player.speed = res, player.speed, player.speed - dashForce, duration)); // change speed back to normal smoothly
        isDashing = false;
        DashCoolDown();

        yield break;
    }

    private void DashCoolDown()
    {
        StartCoroutine(GameManager.ChangeValueSmoothly(result => dash = result, dash, maxDash, baseCoolDown));
        StartCoroutine(GameManager.WaitAndAction(baseCoolDown, () => readyForDash = true));
    }

}
