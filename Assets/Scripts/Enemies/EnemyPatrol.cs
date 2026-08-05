using UnityEngine;

/// <summary>
/// Moves the enemy left and right between two bounds around its starting X,
/// flipping to face its travel direction. Single responsibility: patrol movement.
/// Uses the Rigidbody2D so physics/collisions and gravity still apply.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float patrolDistance = 3f;

    private Rigidbody2D rb;
    private float startX;
    private float direction = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startX = transform.position.x;
    }

    private void FixedUpdate()
    {
        if (direction > 0f && transform.position.x >= startX + patrolDistance)
            SetDirection(-1f);
        else if (direction < 0f && transform.position.x <= startX - patrolDistance)
            SetDirection(1f);

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    private void SetDirection(float newDirection)
    {
        direction = newDirection;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
}
