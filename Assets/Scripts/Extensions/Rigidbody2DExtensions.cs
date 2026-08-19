using UnityEngine;

/// <summary>
/// Extension methods for <see cref="Rigidbody2D"/>.
/// </summary>
public static class Rigidbody2DExtensions
{
    /// <summary>
    /// Returns true when the body is airborne — i.e. there is no ground within
    /// <paramref name="checkDistance"/> directly beneath its collider. The ray starts
    /// just below the collider's feet so it never hits the body itself.
    /// </summary>
    public static bool IsInAir(this Rigidbody2D rb, float checkDistance = 0.15f)
    {
        Collider2D col = rb.GetComponent<Collider2D>();
        if (col == null)
            return true;

        Vector2 origin = new Vector2(col.bounds.center.x, col.bounds.min.y - 0.05f);
        return Physics2D.Raycast(origin, Vector2.down, checkDistance).collider == null;
    }
}
