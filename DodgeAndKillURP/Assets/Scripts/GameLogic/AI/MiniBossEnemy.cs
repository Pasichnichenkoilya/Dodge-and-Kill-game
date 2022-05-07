using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossEnemy : Enemy
{
    protected override void Logic()
    {
        if (!isAgro || target == null)
            return;

        rb.velocity = Vector3.zero;
        LookOnPlayer();

        if (Vector3.Distance(target.position, transform.position) > minDistance)
        {
            Move();
        }

        weapon?.Shoot();
    }
}
