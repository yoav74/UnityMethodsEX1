using UnityEngine;

/// <summary>
/// Base class for player projectiles, implementing the <b>Template Method</b> pattern:
/// <see cref="Fire"/> defines the fixed firing sequence shared by every projectile
/// (prepare → aim → move → post-fire), while the varying steps are supplied by
/// subclasses through the protected hooks. Subclasses fill in <see cref="GetDirection"/>
/// (the one required step) and may override the virtual hooks; they should not override
/// <see cref="Fire"/> itself, so the overall algorithm stays consistent.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected int damage = 1;

    protected Rigidbody2D body;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Template method — the invariant firing algorithm. Runs the steps in a fixed
    /// order; subclasses customise the steps via the hooks below, not this method.
    /// </summary>
    public void Fire()
    {
        Prepare();
        Move(GetDirection());
        OnFired();
    }

    /// <summary>
    /// The one step every projectile must define: the direction it travels when fired.
    /// </summary>
    protected abstract Vector2 GetDirection();

    /// <summary>Reset state just before firing. Override to add setup.</summary>
    protected virtual void Prepare()
    {
        if (body != null)
            body.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Start the projectile moving. Default: constant velocity along
    /// <paramref name="direction"/> at <see cref="speed"/>.
    /// </summary>
    protected virtual void Move(Vector2 direction)
    {
        if (body != null)
            body.linearVelocity = direction.normalized * speed;
    }

    /// <summary>Called after firing. Override for despawn scheduling, logging, etc.</summary>
    protected virtual void OnFired() { }
}
