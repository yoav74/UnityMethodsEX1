using System;

/// <summary>
/// Read-only view of an ammo pool's current amount. Lets UI (or any consumer)
/// depend on an abstraction instead of a concrete ammo store (Dependency Inversion).
/// Deliberately kept ammo-type agnostic so it can be reused (axes, fireballs, ...).
/// </summary>
public interface IAmmoCounter
{
    int Count { get; }

    /// <summary>Raised whenever <see cref="Count"/> changes, passing the new amount.</summary>
    event Action<int> OnAmmoChanged;
}
