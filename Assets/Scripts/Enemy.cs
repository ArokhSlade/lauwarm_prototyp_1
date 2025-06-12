using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] Transform player;

    [SerializeField] float shootInterval;
    [SerializeField] float attackRange = 5f;
    float timeToShoot;

    float AttackReady
    {
        get
        {
            timeToShoot <= 0f;
        }
    }

    void Shoot(Vector3 direction)
    {
        var projectile = Instantiate(projectilePrefab);
        var rb = projectile.GetComponent<Rigidbody>();
        rb.linearVelocity = projectileSpeed * transform.forward;
    }

    void ShootIfClose()
    {
        float playerDistance = (player.position - transform.position).magnitude;

        if (playerDistance < attackRange && AttackReady)
        {
            Shoot();
        }
    }

}
