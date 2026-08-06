using UnityEngine;

/// <summary>
/// Floor/platform tile. Raises a static event when the player lands on top of it
/// (used by PlayerJump to know it is grounded again).
/// </summary>
public class SC_Floor : MonoBehaviour
{
    public delegate void FloorCollisionHandler();
    public static event FloorCollisionHandler OnFloorCollision;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;

        float playerY = col.gameObject.transform.position.y;
        float tileY = transform.position.y;

        // Only counts as "landed" when the player is above the tile's top.
        if (playerY > tileY + 0.45f)
            OnFloorCollision?.Invoke();
    }
}
