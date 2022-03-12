using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float shootingCoolDown; // = 0.5f
    [HideInInspector] public string parentTag;
    protected bool readyForShoot = true;

    protected Vector3 direction;
    protected Quaternion rotationGoal;

    bool IsPaused => GameManager.Instance.PauseManager.IsPaused;

    private void Start()
    {
        readyForShoot = true;
    }

    public virtual void Shoot()
    {
        if (!readyForShoot) return;
        ShootCoolDown();
    }

    public void RotateToTarget(Vector3 targetPosition, float turnSpeed = -1)
    {
        if (turnSpeed == -1)
        {
            gameObject.transform.LookAt(targetPosition);
        }
        else
        {
            direction = (targetPosition - transform.position).normalized;
            rotationGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, turnSpeed);
        }
    }

    protected void ShootCoolDown()
    {
        readyForShoot = false;
        StartCoroutine(GameManager.WaitAndAction(shootingCoolDown, () => readyForShoot = true));
    }
}
