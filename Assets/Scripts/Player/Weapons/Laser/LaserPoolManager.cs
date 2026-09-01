using UnityEngine;

/// <summary>
/// A <see cref="ProjectilePool"/> for laser bolts that creates its instances through the
/// <see cref="LaserFactory"/> (Builder + Director) instead of a plain Instantiate. The
/// laser's tunable values live here (in the Inspector) and are fed through the builder,
/// so the prefab stays the "chassis" and this is the single place to tune the bolt.
/// Everything else — Get/Return, pre-fill — is inherited from the generic pool.
/// </summary>
public class LaserPoolManager : ProjectilePool
{
    [Header("Laser tuning")]
    [SerializeField] private float speed = 14f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float size = 1f;

    private LaserFactory _factory;

    protected override BaseProjectile CreateInstance()
    {
        if (Prefab == null)
            return null;

        if (_factory == null)
            _factory = new LaserFactory(Prefab.gameObject);

        return _factory.Create(speed, lifetime, size);
    }
}
