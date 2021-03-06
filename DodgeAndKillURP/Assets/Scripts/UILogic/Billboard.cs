using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform cam;

    private void Start()
    {
        if(cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
