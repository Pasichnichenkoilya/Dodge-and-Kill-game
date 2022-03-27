using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : Enemy
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
        else
        {
            weapon?.Shoot();
        }
    }
}
