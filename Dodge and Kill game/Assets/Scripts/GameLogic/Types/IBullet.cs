using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBullet
{
    void Launch(Vector3 position, Quaternion rotation, string parentTag);
}
