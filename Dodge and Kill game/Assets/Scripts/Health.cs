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
                StartCoroutine(TakeSpecialDamage(damageType, damage));
            }
            else
            {
                end += specialDamageTakeDuration;
            }
        }

        if (health <= 0) { Destroy(gameObject); return; }

        healthBar.SetHealth(health);
    }

    IEnumerator TakeSpecialDamage(DamageType damageType, float specialDamage)
    {
        isTakingSpecialDamage = true;

        healthBar.SetImageColor(GameManager.Instance.damageMaterialsDictionary[damageType].color);
        end = Time.time + specialDamageTakeDuration;

        while (Time.time < end)
        {

            if (!PauseMenu.IsGamePaused)
                TakeDamage(DamageType.ContactDamage, specialDamage);
            yield return null;
        }
        healthBar.SetDefaultImageColor();

        isTakingSpecialDamage = false;
    }
}
