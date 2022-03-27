using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IPauseHandler
{
    [SerializeField] public Transform target;
    [SerializeField] public Transform moveTarget;
    [SerializeField] bool autoFindTarget;
    [SerializeField] string targetTag = "Player";
    [SerializeField] protected float turnSpeed = 0.015f;
    [SerializeField] public bool isAgro = true;
    [SerializeField] protected float movingSpeed = 5f;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float minDistance = 10f;

    [SerializeField] protected Weapon weapon;

    [SerializeField] GameObject walkingParticles;

    Quaternion rotationGoal;
    Vector3 direction;
    Vector3 tmpVelocity;

    private void Start()
    {
        GameManager.Instance.PauseManager.Subscribe(this);

        if (autoFindTarget && GameManager.Instance.Player != null)
        {
            var player = GameManager.Instance.Player;
            target = player.transform;
            moveTarget = player.transform;
        }

        if (weapon != null)
            weapon.parentTag = gameObject.tag.ToString();

        if (walkingParticles != null)
            Instantiate(walkingParticles, transform);

        if (moveTarget == null)
            moveTarget = target;
    }
    private void Update()
    {
        if (GameManager.Instance.PauseManager.IsPaused)
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
        tmpVelocity = rb.velocity;
    }

    protected void LookOnPlayer()
    {
        if (target == null) return;

        direction = (target.position - transform.position).normalized;
        rotationGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, turnSpeed);

        weapon?.RotateToTarget(target.position, turnSpeed);
    }

    public void SetPaused(bool isPaused, bool showPauseUI = true)
    {
        rb.velocity = isPaused ? Vector3.zero : tmpVelocity;
    }
}
