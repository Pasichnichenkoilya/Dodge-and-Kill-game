using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] HealthBar healthBar;
    [SerializeField] bool godMode = false;

    [SerializeField] ParticleSystem onDestroyParticles;
    [SerializeField] int moneyDropAmount = 10;

    [SerializeField] ParticleSystem moneyParticles;
    [SerializeField] Material onDestroyParticlesMaterial;

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

        if (onDestroyParticlesMaterial == null)
        {
            onDestroyParticlesMaterial = gameObject.GetComponent<Material>();
        }
    }

    public void TakeDamage(DamageType damageType, float damage)
    {
        if (godMode) return;

        healthBar.SetVisible(true);

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
        if (gameObject.GetComponent<IPauseHandler>() is IPauseHandler handler && handler != null)
            GameManager.Instance.PauseManager.Unsubscribe(handler);

        var particles = Instantiate(onDestroyParticles, transform.position, transform.rotation);
        particles.startColor = onDestroyParticlesMaterial.color;

        if (moneyParticles != null)
        {
            particles = Instantiate(moneyParticles, transform.position, transform.rotation);
            particles.emission.SetBurst(0, new ParticleSystem.Burst(0, moneyDropAmount));
        }

        Destroy(gameObject);
    }

    IEnumerator TakeSpecialDamage(DamageType damageType, float specialDamage)
    {
        isTakingSpecialDamage = true;

        healthBar.SetImageColor(GameManager.Instance.damageMaterialsDictionary[damageType].color);
        end = 0;
        while (end < specialDamageTakeCount)
        {
            if (!PauseMenu.IsGamePaused)
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
