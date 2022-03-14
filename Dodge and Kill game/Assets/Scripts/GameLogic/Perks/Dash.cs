using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Perk
{
    public ManaBar perkBar;
    [SerializeField] Player player;

    [SerializeField] float dashForce = 10f;
    [SerializeField] float maxDash = 1f;

    [SerializeField] ParticleSystem dashParticlesPrefab;
    Quaternion particlesRotation = Quaternion.identity;
    ParticleSystem dashParticles;

    bool readyForDash = true;
    public bool isDashing;
    float dash;

    private void Start()
    {
        isDashing = false;
        dash = maxDash;
        perkBar.SetMaxValue(maxDash);

        dashParticles = Instantiate(dashParticlesPrefab, player.transform);
    }

    private void Update()
    {
        perkBar.SetValue(dash);
    }

    void LateUpdate()
    {
        dashParticles.transform.rotation = particlesRotation;
    }

    public override void Action()
    {
        if (readyForDash && player.moveDelta.magnitude > 0)
        {
            StartCoroutine(Dashing());
        }
    }

    private IEnumerator Dashing()
    {
        isDashing = true;
        readyForDash = false;
        player.speed += dashForce;

        var vector = new Vector3(player.moveDelta.x, 0, player.moveDelta.z) * -1;
        particlesRotation = Quaternion.LookRotation(vector);

        dashParticles.Play();

        StartCoroutine(GameManager.ChangeValueSmoothly(res => dash = res, dash, 0, duration));//change dash ui to 0 smoothly
        yield return new WaitForSeconds(duration);

        StartCoroutine(GameManager.ChangeValueSmoothly(res => player.speed = res, player.speed, player.speed - dashForce, duration)); // change speed back to normal smoothly
        DashCoolDown();
        isDashing = false;

        yield break;
    }

    private void DashCoolDown()
    {
        StartCoroutine(GameManager.ChangeValueSmoothly(result => dash = result, dash, maxDash, baseCoolDown));
        StartCoroutine(GameManager.WaitAndAction(baseCoolDown, () => readyForDash = true));
    }

}