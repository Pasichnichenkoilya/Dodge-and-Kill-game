using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmWeapon : Weapon
{

    public PooledObjectTag objectTag;
    [SerializeField] Transform shootFrom;

    public override void Shoot()
    {
        if (!readyForShoot) return;

        var bullet = GameManager.Instance.poolDictionary[objectTag].Get();
        bullet.GetComponent<IBullet>().Launch(shootFrom.position, transform.rotation, parentTag);

        base.Shoot();
    }
}
