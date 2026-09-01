using UnityEngine;

/// <summary>
/// The fireball projectile: flies horizontally in the direction it is fired. Inherits
/// the shared firing sequence from <see cref="BaseProjectile"/> and customises only the
/// facing and the horizontal launch impulse.
/// </summary>
public class ProjectileFireball : BaseProjectile
{
    protected override void Prepare(Vector2 direction)
    {
        base.Prepare(direction);
        float facing = Mathf.Sign(direction.x);
        transform.localScale = new Vector3(facing, 1f, 1f);
    }

    protected override void Launch(Vector2 direction)
    {
        if (body != null)
            body.AddForce(new Vector2(Mathf.Sign(direction.x) * speed, 0f));
    }
}
