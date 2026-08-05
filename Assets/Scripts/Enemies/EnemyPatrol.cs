using UnityEngine;

/// <summary>
/// Moves the enemy left and right between two bounds around its starting X,
/// flipping to face its travel direction. Also reverses if it gets stuck against
/// terrain (a wall) for a short time, so it never freezes while pushing into it.
/// Single responsibility: patrol movement. Uses the Rigidbody2D so physics and
/// gravity still apply.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float patrolDistance = 3f;
    [SerializeField] private float stuckTime = 0.5f; // reverse after being blocked this long

    private Rigidbody2D rb;
    private float startX;
    private float direction = 1f;
    private float lastX;
    private float stuckTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startX = transform.position.x;
        lastX = startX;
    }

    private void FixedUpdate()
    {
        // Reverse at the edges of the patrol range.
        if (direction > 0f && transform.position.x >= startX + patrolDistance)
            SetDirection(-1f);
        else if (direction < 0f && transform.position.x <= startX - patrolDistance)
            SetDirection(1f);

        // Reverse if blocked by terrain: it isn't making the horizontal progress we asked for.
        float moved = Mathf.Abs(transform.position.x - lastX);
        float expected = Mathf.Abs(speed) * Time.fixedDeltaTime * 0.5f;
        if (moved < expected)
        {
            stuckTimer += Time.fixedDeltaTime;
            if (stuckTimer >= stuckTime)
                SetDirection(-direction);
        }
        else
        {
            stuckTimer = 0f;
        }

        lastX = transform.position.x;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    private void SetDirection(float newDirection)
    {
        direction = newDirection;
        stuckTimer = 0f; // fresh start after turning around

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
}
