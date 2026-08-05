using UnityEngine;

/// <summary>
/// Deals damage to any <see cref="IDamageable"/> this object collides with, then
/// (optionally) destroys itself. Attach to projectiles so weapons can hurt any
/// damageable target without knowing its concrete type (Open/Closed). Non-damageable
/// hits (walls, the player) are ignored, so a projectile never hurts its owner.
/// </summary>
public class DamageOnHit : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private bool destroyOnHit = true;

    private bool hasHit;

    private void OnCollisionEnter2D(Collision2D collision) => TryHit(collision.collider);

    private void OnTriggerEnter2D(Collider2D other) => TryHit(other);

    private void TryHit(Collider2D other)
    {
        if (hasHit)
            return;

        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        if (damageable == null)
            return;

        hasHit = true;
        damageable.TakeDamage(damage);

        if (destroyOnHit)
            Destroy(gameObject);
    }
}
