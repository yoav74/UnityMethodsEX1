using UnityEngine;

/// <summary>
/// A platform tile that ping-pongs horizontally between its start X and
/// <see cref="distance"/> tiles on each side. It moves as a Kinematic Rigidbody2D
/// via MovePosition so the moving collider stays smooth (no jitter or shoving), and
/// while the player stands on it, it feeds its own velocity into
/// <see cref="PlayerMovement"/> so the player is carried along. Single
/// responsibility: platform movement + carrying its rider.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float distance = 2f; // tiles to each side (1 unit = 1 tile)
    [SerializeField] private float speed = 2f;
    [SerializeField] private string playerTag = "Player";

    private Rigidbody2D rb;
    private float startX;
    private int direction = 1;
    private PlayerMovement rider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        startX = transform.position.x;
    }

    private void FixedUpdate()
    {
        if (direction > 0 && rb.position.x >= startX + distance)
            direction = -1;
        else if (direction < 0 && rb.position.x <= startX - distance)
            direction = 1;

        float velocityX = direction * speed;
        rb.MovePosition(rb.position + new Vector2(velocityX * Time.fixedDeltaTime, 0f));

        // Carry the player: feed the platform's velocity into their movement.
        if (rider != null)
            rider.SetPlatformVelocity(velocityX);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag(playerTag))
            return;

        // Only carry the player when they land on top, not when they bump a side.
        if (collision.collider.transform.position.y > transform.position.y)
            rider = collision.collider.GetComponentInParent<PlayerMovement>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (rider != null && collision.collider.CompareTag(playerTag))
        {
            rider.SetPlatformVelocity(0f); // stop carrying when they step off
            rider = null;
        }
    }
}
