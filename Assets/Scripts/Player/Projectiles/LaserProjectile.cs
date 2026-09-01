using UnityEngine;

/// <summary>
/// A laser bolt that always fires straight up, regardless of which way the player
/// faces. As a <see cref="BaseProjectile"/> it reuses the shared firing sequence and
/// only overrides the direction step (the Template Method hook). Its full runtime
/// behaviour — damaging enemies and returning to the pool on hit — is added in the
/// Laser Shooting ticket (ME-26).
/// </summary>
public class LaserProjectile : BaseProjectile
{
    protected override Vector2 ResolveDirection(Vector2 aim) => Vector2.up;
}
