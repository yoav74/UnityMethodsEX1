using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic <b>object pool</b> for projectiles: pre-creates a reserve of
/// <see cref="BaseProjectile"/> instances from a prefab and reuses them instead of
/// Instantiate/Destroy on every shot. <see cref="Get"/> hands one out (or null when the
/// fixed reserve is exhausted); <see cref="Return"/> takes one back. A weapon that wants
/// pooling calls <see cref="Get"/>; the projectile returns itself via
/// <see cref="BaseProjectile.Despawn"/> when it hits something or its lifetime ends.
/// Single responsibility: pooling. Subclasses override <see cref="CreateInstance"/> to
/// change how a fresh instance is produced (e.g. the laser builds via a factory).
/// </summary>
public class ProjectilePool : MonoBehaviour, IProjectilePool
{
    [SerializeField] private BaseProjectile prefab;
    [SerializeField] private int initialSize = 10;

    private readonly Queue<BaseProjectile> _available = new Queue<BaseProjectile>();

    /// <summary>The prefab this pool builds from (for subclasses that create differently).</summary>
    protected BaseProjectile Prefab => prefab;

    protected virtual void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            BaseProjectile projectile = CreatePooled();
            if (projectile != null)
                _available.Enqueue(projectile);
        }
    }

    /// <summary>
    /// Takes a projectile from the pool, or returns null when the pool is exhausted — it
    /// is a fixed reserve and never instantiates beyond <c>initialSize</c>, so the caller
    /// simply skips the shot.
    /// </summary>
    public BaseProjectile Get()
    {
        if (_available.Count == 0)
        {
            Debug.Log($"{name}: pool empty — shot skipped");
            return null;
        }

        BaseProjectile projectile = _available.Dequeue();
        projectile.transform.SetParent(null); // fired projectiles live in the world
        projectile.gameObject.SetActive(true);
        Debug.Log($"{name}: taken from pool ({_available.Count} still available)");
        return projectile;
    }

    /// <summary>Returns a spent projectile to the pool for reuse.</summary>
    public void Return(BaseProjectile projectile)
    {
        if (projectile == null)
            return;

        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        _available.Enqueue(projectile);
        Debug.Log($"{name}: returned to pool ({_available.Count} available)");
    }

    /// <summary>
    /// Finds the active pool that serves projectiles of type <typeparamref name="T"/> —
    /// used by a weapon to locate its pool at runtime, since a prefab-based weapon can't
    /// hold an Inspector reference to a scene pool anyway (mirrors the PlayerLocator idea).
    /// </summary>
    public static ProjectilePool FindFor<T>() where T : BaseProjectile
    {
        foreach (ProjectilePool candidate in FindObjectsByType<ProjectilePool>(FindObjectsInactive.Exclude))
            if (candidate.prefab is T)
                return candidate;

        return null;
    }

    /// <summary>How a fresh instance is produced. Default: Instantiate the prefab.</summary>
    protected virtual BaseProjectile CreateInstance()
    {
        return prefab != null ? Instantiate(prefab) : null;
    }

    private BaseProjectile CreatePooled()
    {
        BaseProjectile projectile = CreateInstance();
        if (projectile == null)
            return null;

        projectile.SetPool(this);
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform);
        return projectile;
    }
}
