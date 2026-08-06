using UnityEngine;

/// <summary>
/// Fires a projectile every so often. Single responsibility: timed shooting. The
/// interval is randomized between <see cref="minInterval"/> and
/// <see cref="maxInterval"/> (set them equal for a fixed rate). It launches the
/// spawned projectile by setting its Rigidbody2D velocity, and does not itself know
/// what the projectile does on impact (that is <see cref="EnemyProjectile"/>).
/// </summary>
public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Vector2 fireDirection = Vector2.left;
    [SerializeField] private float projectileSpeed = 6f;
    [SerializeField] private float minInterval = 1.5f;
    [SerializeField] private float maxInterval = 3f;

    private float timer;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        timer = Random.Range(minInterval, Mathf.Max(minInterval, maxInterval));
    }

    private void Shoot()
    {
        if (projectilePrefab == null)
            return;

        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = fireDirection.normalized * projectileSpeed;
    }
}
