using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float turnSpeed = 0.015f;
    [SerializeField] protected bool isAgro = true;
    [SerializeField] protected float movingSpeed = 5f;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float minDistance = 10f;

    [SerializeField] protected Weapon weapon;

    Quaternion rotationGoal;
    Vector3 direction;

    private void Start()
    {
        if (weapon != null)
            weapon.parentTag = gameObject.tag.ToString();
    }
    private void Update()
    {
        if (PauseMenu.IsGamePaused)
            return;

        Logic();
    }

    protected virtual void Logic()
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

    protected void Move()
    {
        rb.velocity = transform.forward * movingSpeed;
    }

    protected void LookOnPlayer()
    {
        if (target == null) return;

        direction = (target.position - transform.position).normalized;
        rotationGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, turnSpeed);

        weapon?.RotateToTarget(target.position, turnSpeed);
    }
}
