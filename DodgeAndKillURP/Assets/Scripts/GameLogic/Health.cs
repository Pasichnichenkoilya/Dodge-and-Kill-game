using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] HealthBar healthBar;
    [SerializeField] bool godMode = false;

    [SerializeField] ParticleSystem moneyParticles;
    [SerializeField] Material material;

    Material damageMaterial;

    [SerializeField] Dissolve dissolve;
    [SerializeField] float dissolveSpeed = 1f;

    public bool IsDead = false;

    float health;
    float end;
    bool isTakingSpecialDamage;
    float specialDamageTakeDuration = 3f;
    float specialDamageTakeCount = 0;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        end = 0;
        isTakingSpecialDamage = false;

        if (material == null)
        {
            material = gameObject.GetComponent<Material>();
        }

        moneyParticles = GameManager.Instance.moneyDropParticles;
    }

    public void TakeDamage(DamageType damageType, float damage, Material damageMaterial = null)
    {
        if (godMode) return;

        healthBar.SetVisible(true);
        this.damageMaterial = damageMaterial;

        if (damageType == DamageType.ContactDamage)
        {
            health -= damage;
        }
        else
        {
            if (!isTakingSpecialDamage)
            {
                specialDamageTakeCount += specialDamageTakeDuration;
                StartCoroutine(TakeSpecialDamage(damageType, damage));
            }
            else
            {
                specialDamageTakeCount += specialDamageTakeDuration;
            }
        }

        if (health <= 0)
            Die();

        healthBar.SetHealth(health);
    }

    private void Die()
    {
        if (IsDead)
            return;

        IsDead = true;

        if (gameObject.GetComponent<Enemy>() is Enemy e)
            e.isAgro = false;
        
        dissolve.StartDissolve();

        if (gameObject.GetComponent<IPauseHandler>() is IPauseHandler handler && handler != null)
        {
            //handler.SetPaused(true);
            GameManager.Instance.PauseManager.Unsubscribe(handler);
        }

        //Destroy(gameObject, dissolve.dissolveTime+dissolve.dissolveTime*0.5f);
    }

    IEnumerator TakeSpecialDamage(DamageType damageType, float specialDamage)
    {
        isTakingSpecialDamage = true;

        healthBar.SetImageColor(GameManager.Instance.damageMaterialsDictionary[damageType].color);
        end = 0;
        while (end < specialDamageTakeCount)
        {
            if (!GameManager.Instance.PauseManager.IsPaused)
            {
                TakeDamage(DamageType.ContactDamage, specialDamage);
                end += Time.deltaTime;
            }
            yield return null;
        }
        healthBar.SetDefaultImageColor();
        specialDamageTakeCount = 0;

        isTakingSpecialDamage = false;
    }

    public void SetGodMode(bool godMode)
    {
        this.godMode = godMode;
    }
}
