using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float contactDamage = 20;
    [SerializeField] protected float specialDamage = 0;
    [SerializeField] protected float lifeTime = 5f;
    [SerializeField] protected PooledObjectTag objectTag;
    [SerializeField] public DamageType damageType;

    [HideInInspector] public string parentTag;

    public void Launch(Vector3 position, Quaternion rotation, string parentTag)
    {
        transform.position = position;
        transform.rotation = rotation;
        this.parentTag = parentTag;

        rb.velocity = transform.forward * speed;
        StartCoroutine(LifeTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(parentTag) && other.gameObject.GetComponent<Health>() is Health healthObj)
        {
            healthObj.TakeDamage(DamageType.ContactDamage, contactDamage);
            healthObj.TakeDamage(damageType, specialDamage);

            GameManager.Instance.poolDictionary[objectTag].Release(gameObject);
        }
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        GameManager.Instance.poolDictionary[objectTag].Release(gameObject);
        yield break;
    }
}