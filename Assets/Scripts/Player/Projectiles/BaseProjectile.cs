using UnityEngine;

/// <summary>
/// Base class for projectiles, implementing the <b>Template Method</b> pattern:
/// <see cref="Fire"/> defines the fixed firing sequence — resolve direction → prepare →
/// launch → schedule despawn → post-fire — while each projectile type customises the
/// individual steps through the protected hooks. Subclasses never override
/// <see cref="Fire"/> itself, so every projectile fires through the same sequence and a
/// new projectile type is added by extension, not by changing this class (Open/Closed).
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifetime = 3f;

    protected Rigidbody2D body;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Template method — the invariant firing sequence. <paramref name="aim"/> is the
    /// requested direction (e.g. the way the shooter faces); a fixed-direction
    /// projectile ignores it via <see cref="ResolveDirection"/>.
    /// </summary>
    public void Fire(Vector2 aim)
    {
        Vector2 direction = ResolveDirection(aim);
        Prepare(direction);
        Launch(direction);
        ScheduleDespawn();
        OnFired(direction);
    }

    /// <summary>Fire a fixed-direction projectile (the aim is supplied by the type itself).</summary>
    public void Fire() => Fire(Vector2.zero);

    /// <summary>Set the shared motion parameters. Used by builders/factories that
    /// assemble a projectile in code rather than from Inspector values.</summary>
    public void Configure(float projectileSpeed, float projectileLifetime)
    {
        speed = projectileSpeed;
        lifetime = projectileLifetime;
    }

    /// <summary>Map the requested aim to the actual travel direction. Default: use it as-is.</summary>
    protected virtual Vector2 ResolveDirection(Vector2 aim) => aim;

    /// <summary>Reset/orient the projectile just before launch. Default: clear velocity.</summary>
    protected virtual void Prepare(Vector2 direction)
    {
        if (body != null)
            body.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Apply motion. Default: constant velocity along <paramref name="direction"/> at
    /// <see cref="speed"/>.
    /// </summary>
    protected virtual void Launch(Vector2 direction)
    {
        if (body != null)
            body.linearVelocity = direction.normalized * speed;
    }

    /// <summary>Schedule self-despawn. Default: destroy after <see cref="lifetime"/> (≤0 = never).</summary>
    protected virtual void ScheduleDespawn()
    {
        if (lifetime > 0f)
            Destroy(gameObject, lifetime);
    }

    /// <summary>Post-fire hook (spin, logging, etc.). Default: nothing.</summary>
    protected virtual void OnFired(Vector2 direction) { }
}
