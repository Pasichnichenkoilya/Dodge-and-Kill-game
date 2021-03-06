using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour, IPauseHandler
{
    public float speed = 5f;

    public Rigidbody rb;
    public Weapon weapon;
    public Perk perk;

    [HideInInspector] public Vector3 moveDelta;

    [SerializeField] GameObject walkingParticles;

    Vector3 tmpVelocity;

    private void Start()
    {
        GameManager.Instance.PauseManager.Subscribe(this);

        moveDelta = new Vector3();

        if (weapon != null)
            weapon.parentTag = gameObject.tag.ToString();

        Instantiate(walkingParticles, transform);
    }

    void Update()
    {
        if (GameManager.Instance.PauseManager.IsPaused || gameObject.GetComponent<Health>().IsDead)
            return;

        Move();
        RotateToCursor();

        if (Input.GetKey(KeyCode.Mouse0))
            weapon?.Shoot();

        if (Input.GetKeyDown(KeyCode.Space))
            perk.Action();
    }

    #region Movement and rotation
    private void RotateToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.LookAt(new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));
            weapon?.RotateToTarget(new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z));
        }
    }

    private void Move()
    {
        moveDelta.z = Input.GetAxisRaw("Vertical");
        moveDelta.x = Input.GetAxisRaw("Horizontal");

        rb.velocity = moveDelta.normalized * speed;
        tmpVelocity = rb.velocity;
    }

    #endregion
    public void SetPaused(bool isPaused, bool showPauseUI = true)
    {
        rb.velocity = isPaused ? Vector3.zero : tmpVelocity;
    }

}
