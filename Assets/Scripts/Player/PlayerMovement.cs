using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Color boostColor = Color.yellow;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Color originalColor = Color.white;
    private float speedMultiplier = 1f;
    private Coroutine boostRoutine;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite != null)
            originalColor = sprite.color;
    }

    private void FixedUpdate()
    {
        float direction = ReadDirection();

        // Drive horizontal velocity directly every step so the player walks only
        // while a key is held and stops immediately on release (no leftover momentum).
        if (rigid != null)
            rigid.linearVelocity = new Vector2(direction * speed * speedMultiplier, rigid.linearVelocity.y);

        if (direction > 0f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction < 0f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    /// <summary>
    /// Temporarily multiplies movement speed (and tints the sprite) for
    /// <paramref name="duration"/> seconds. Picking up another boost refreshes the timer.
    /// </summary>
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (boostRoutine != null)
            StopCoroutine(boostRoutine);

        boostRoutine = StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        speedMultiplier = multiplier;
        if (sprite != null)
            sprite.color = boostColor;

        yield return new WaitForSeconds(duration);

        speedMultiplier = 1f;
        if (sprite != null)
            sprite.color = originalColor;

        boostRoutine = null;
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
