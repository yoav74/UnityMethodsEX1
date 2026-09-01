using UnityEngine;

/// <summary>
/// A laser bolt that always fires straight up, regardless of which way the player
/// faces. As a <see cref="BaseProjectile"/> it reuses the shared firing sequence and
/// only overrides the direction step (the Template Method hook). Its hit + return-to-pool
/// behaviour is added in the Laser Shooting ticket (ME-26); like every projectile it
/// deals a flat 1 point of damage.
/// </summary>
public class LaserProjectile : BaseProjectile
{
    protected override Vector2 ResolveDirection(Vector2 aim) => Vector2.up;
}
