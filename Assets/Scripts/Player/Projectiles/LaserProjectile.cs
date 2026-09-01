using UnityEngine;

/// <summary>
/// A laser bolt that fires straight up. As a <see cref="BaseProjectile"/> it inherits
/// the shared <see cref="BaseProjectile.Fire"/> sequence and only supplies its own
/// travel direction here (the Template Method hook). Its full runtime behaviour —
/// damaging enemies and returning to the pool on hit — is added in the Laser Shooting
/// ticket (ME-26).
/// </summary>
public class LaserProjectile : BaseProjectile
{
    protected override Vector2 GetDirection() => Vector2.up;
}
