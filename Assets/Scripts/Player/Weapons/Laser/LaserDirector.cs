/// <summary>
/// <b>Director</b> for the Builder pattern: drives a <see cref="LaserBuilder"/> through the
/// fixed construction sequence (speed → lifetime → size → build) using the values it is
/// given, so callers get a consistently-assembled bolt without repeating the steps. The
/// values themselves come from the pool, so they can be tuned in the Inspector.
/// </summary>
public class LaserDirector
{
    private readonly LaserBuilder _builder;

    public LaserDirector(LaserBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>Assembles a laser bolt, step by step, from the given values.</summary>
    public LaserProjectile Build(float speed, float lifetime, float size)
    {
        return _builder
            .SetSpeed(speed)
            .SetLifetime(lifetime)
            .SetSize(size)
            .Build();
    }
}
