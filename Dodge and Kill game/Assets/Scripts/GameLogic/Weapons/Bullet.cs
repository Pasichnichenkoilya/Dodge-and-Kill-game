using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet, IPauseHandler, IPooledItem
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float contactDamage = 20;
    [SerializeField] protected float specialDamage = 0;
    [SerializeField] protected float lifeTime = 5f;
    [SerializeField] protected PooledObjectTag objectTag;
    [SerializeField] public DamageType damageType;
    [SerializeField] protected ParticleSystem onHitParticlesPrefab;

    [HideInInspector] public string parentTag;

    Vector3 tmpVelocity;

    private void Start()
    {
        GameManager.Instance.PauseManager.Subscribe(this);
    }

    public void Launch(Vector3 position, Quaternion rotation, string parentTag)
    {
        transform.position = position;
        transform.rotation = rotation;
        this.parentTag = parentTag;

        rb.velocity = transform.forward * speed;
        tmpVelocity = rb.velocity;

        LifeTime();
    }

    private void LifeTime()
    {
        StartCoroutine(GameManager.WaitAndAction(
            lifeTime,
            () => Release()
            ));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(parentTag) && other.gameObject.GetComponent<Health>() is Health healthObj)
        {
            healthObj.TakeDamage(DamageType.ContactDamage, contactDamage);
            healthObj.TakeDamage(damageType, specialDamage);

            var particles = Instantiate(onHitParticlesPrefab, healthObj.transform);
            particles.startColor = GameManager.Instance.damageMaterialsDictionary[damageType].color;

            Release();
        }
    }

    public void SetPaused(bool isPaused)
    {
        rb.velocity = isPaused ? Vector3.zero : tmpVelocity;
    }

    public void Release()
    {
        GameManager.Instance.poolDictionary[objectTag].Release(gameObject);
    }
}