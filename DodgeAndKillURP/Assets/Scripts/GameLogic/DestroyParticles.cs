using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DestroyParticles : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 5);
    }
}