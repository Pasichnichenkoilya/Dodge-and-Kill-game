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

    float health;
    float end;
    bool isTakingSpecialDamage;
    float specialDamageTakeDuration = 3f;

    float specialDamageTakeCount = 0;
    float startWaiting;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        end = 0;
        isTakingSpecialDamage = false;
    }

    public void TakeDamage(DamageType damageType, float damage)
    {
        if (godMode) return;

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

        if (health <= 0) {
            Die(); 
        }

        healthBar.SetHealth(health);
    }

    private void Die()
    {
        if (gameObject.GetComponent<IPauseHandler>() is IPauseHandler handler && handler != null)
        {
            GameManager.Instance.PauseManager.Unsubscribe(handler);
        }
        Destroy(gameObject);
    }

    IEnumerator TakeSpecialDamage(DamageType damageType, float specialDamage)
    {
        isTakingSpecialDamage = true;

        healthBar.SetImageColor(GameManager.Instance.damageMaterialsDictionary[damageType].color);
        //end = Time.time + specialDamageTakeDuration;
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
}
