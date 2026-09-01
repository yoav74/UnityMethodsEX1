using UnityEngine;

/// <summary>
/// <b>Factory</b> for laser bolts: hands out ready-to-fire <see cref="LaserProjectile"/>
/// instances so callers (the pool) get a finished laser from a single <see cref="Create"/>
/// call without knowing about the builder/director assembly steps. It owns the laser
/// prefab and wires up the builder + director once.
/// </summary>
public class LaserFactory
{
    private readonly LaserDirector _director;

    /// <param name="laserPrefab">The Laser prefab (with a <see cref="LaserProjectile"/>)
    /// that every bolt is built from.</param>
    public LaserFactory(GameObject laserPrefab)
    {
        _director = new LaserDirector(new LaserBuilder(laserPrefab));
    }

    /// <summary>Creates a ready, fully configured laser bolt from the given values.</summary>
    public LaserProjectile Create(float speed, float lifetime, float size)
    {
        return _director.Build(speed, lifetime, size);
    }
}
