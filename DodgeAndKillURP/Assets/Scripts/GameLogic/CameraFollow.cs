using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSpeed = 0.125f;
    [SerializeField] float height = 25f;
    [SerializeField] Vector3 offset;
    [SerializeField] float f;
    public float maxDistance = 25;

    void FixedUpdate()
    {
        if (target == null || GameManager.Instance.PauseManager.IsPaused)
            return;

        Vector3 dp1 = new Vector3(target.position.x, height, target.position.z);
        Vector3 dp = Vector3.Lerp(transform.position, dp1, followSpeed);
        transform.LookAt(target);
        transform.position = dp;
        transform.rotation = Quaternion.Euler(90, 0, 0);
        
        transform.position += target.forward * f;

        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 x = Vector3.zero;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            x = hit.point;
        }

        var delta = Vector2.ClampMagnitude(target.position - x, maxDistance);

        var distance = Vector3.Distance(target.position, x);

        Debug.Log(distance);*/
    }
}
