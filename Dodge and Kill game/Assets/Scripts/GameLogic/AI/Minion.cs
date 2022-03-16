using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{
    protected override void Logic()
    {
        LookOnPlayer();
        weapon?.Shoot();
    }
}
