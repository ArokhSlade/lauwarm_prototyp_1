using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] Transform target;

    [SerializeField] float shootInterval;
    [SerializeField] float attackRange = 5f;
    float timeToShoot;

    void FixedUpdate()
    {
        ShootIfClose();
    }

    void Start()
    {
        timeToShoot = shootInterval;
    }

    bool AttackReady
    {
        get
        {
            if (timeToShoot <= 0f)
            {
                timeToShoot += shootInterval;
                return true;
            }

            return false;
        }
    }

    void Shoot()
    {
        var projectile = Instantiate(projectilePrefab);
        var rb = projectile.GetComponent<Rigidbody>();
        rb.transform.position = transform.position;
        rb.transform.LookAt(target);
        rb.linearVelocity = projectileSpeed * rb.transform.forward;
    }

    void ShootIfClose()
    {
        float targetDistance = (target.position - transform.position).magnitude;

        if (targetDistance < attackRange)
        {
            timeToShoot -= Time.deltaTime;

            if (AttackReady)
            {
                Shoot();
            }
        }
    }

}
