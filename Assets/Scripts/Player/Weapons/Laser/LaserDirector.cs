/// <summary>
/// <b>Director</b> for the Builder pattern: drives a <see cref="LaserBuilder"/> through a
/// fixed recipe so callers get a consistent, standard laser bolt without repeating the
/// individual build steps. Swapping in a different recipe (e.g. a faster laser) is a
/// change here, not in every call site.
/// </summary>
public class LaserDirector
{
    private readonly LaserBuilder _builder;

    public LaserDirector(LaserBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>Assembles the standard laser bolt, step by step, through the builder.</summary>
    public LaserProjectile BuildStandardLaser()
    {
        return _builder
            .SetSpeed(14f)
            .SetLifetime(2f)
            .SetSize(1f)
            .Build();
    }
}
