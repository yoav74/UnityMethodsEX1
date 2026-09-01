/// <summary>
/// A <see cref="ProjectilePool"/> for laser bolts that creates its instances through the
/// <see cref="LaserFactory"/> (Builder + Director) instead of a plain Instantiate, so the
/// pool is filled with fully-assembled lasers. Everything else — Get/Return, pre-fill,
/// growth — is inherited from the generic pool.
/// </summary>
public class LaserPoolManager : ProjectilePool
{
    private LaserFactory _factory;

    protected override BaseProjectile CreateInstance()
    {
        if (Prefab == null)
            return null;

        if (_factory == null)
            _factory = new LaserFactory(Prefab.gameObject);

        return _factory.Create();
    }
}
