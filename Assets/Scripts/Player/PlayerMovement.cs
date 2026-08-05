using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float direction = ReadDirection();

        // Drive horizontal velocity directly every step so the player walks only
        // while a key is held and stops immediately on release (no leftover
        // momentum / "push"). Vertical velocity is left to gravity and jumping.
        if (rigid != null)
            rigid.linearVelocity = new Vector2(direction * speed, rigid.linearVelocity.y);

        if (direction > 0f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction < 0f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private float ReadDirection()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null)
            return 0f;

        float direction = 0f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)
            direction -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed)
            direction += 1f;

        return direction;
    }
}
