/// <summary>
/// A pool a projectile can return itself to when it is spent. Kept minimal (just
/// <see cref="Return"/>) so a <see cref="BaseProjectile"/> depends on the abstraction,
/// not on a concrete pool (Dependency Inversion / Interface Segregation).
/// </summary>
public interface IProjectilePool
{
    void Return(BaseProjectile projectile);
}
