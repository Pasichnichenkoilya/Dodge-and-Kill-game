using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon, IPauseHandler
{
    [SerializeField] Transform shootFrom;
    [SerializeField] float attackRange;
    [SerializeField] float contactDamage;
    [SerializeField] DamageType damageType;
    [SerializeField] float specialDamage = 0.05f;

    [SerializeField] Animator animator;

    float prevSpeed;

    private void Start()
    {
        GameManager.Instance.PauseManager.Subscribe(this);
        prevSpeed = animator.speed;
    }

    public override void Shoot()
    {
        if (!readyForShoot)
            return;

        animator.SetTrigger("Attack");

        base.Shoot();
    }

    public void ApplyDamage()
    {
        var colliders = Physics.OverlapSphere(shootFrom.position, attackRange);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player") && collider.gameObject.GetComponent<Health>() is Health healthObj)
            {
                healthObj.TakeDamage(DamageType.ContactDamage, contactDamage);
                healthObj.TakeDamage(damageType, specialDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(shootFrom.position, attackRange);
    }

    private void OnDestroy()
    {
        GameManager.Instance.PauseManager.Unsubscribe(this);
    }

    public void SetPaused(bool isPaused, bool showPauseUI = true)
    {
        if (isPaused)
        {
            animator.speed = 0;
        }
        else
        {
            animator.speed = prevSpeed;
        }
    }
}
