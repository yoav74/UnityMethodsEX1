using UnityEngine;

/// <summary>
/// Fires a projectile every so often. Single responsibility: timed shooting. The
/// interval is randomized between <see cref="minInterval"/> and
/// <see cref="maxInterval"/> (set them equal for a fixed rate). It spawns from the
/// muzzle that matches the fire direction (left/right chamber), launches the
/// projectile via its Rigidbody2D, and flips its sprite to face that direction.
/// </summary>
public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform leftFirePoint;
    [SerializeField] private Transform rightFirePoint;
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

        Transform muzzle = SelectMuzzle();
        Vector3 spawnPosition = muzzle != null ? muzzle.position : transform.position;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        FaceFireDirection(projectile);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = fireDirection.normalized * projectileSpeed;
    }

    /// <summary>Picks the chamber on the side the shot is heading, falling back to the other.</summary>
    private Transform SelectMuzzle()
    {
        if (fireDirection.x > 0f)
            return rightFirePoint != null ? rightFirePoint : leftFirePoint;
        if (fireDirection.x < 0f)
            return leftFirePoint != null ? leftFirePoint : rightFirePoint;

        return leftFirePoint != null ? leftFirePoint : rightFirePoint; // no horizontal component
    }

    /// <summary>Mirrors the projectile's sprite so it faces the way it is fired.</summary>
    private void FaceFireDirection(GameObject projectile)
    {
        if (Mathf.Approximately(fireDirection.x, 0f))
            return;

        SpriteRenderer sprite = projectile.GetComponentInChildren<SpriteRenderer>();
        if (sprite == null)
            return;

        // The projectile art faces left by default, so mirror it when firing right.
        sprite.flipX = fireDirection.x > 0f;
    }
}
