using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Lets the player jump from the ground and perform one extra jump in mid-air
/// (a double jump). Landing on a floor (via <see cref="SC_Floor"/>) refreshes the
/// jump count.
/// </summary>
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 100f;
    [SerializeField] private int maxJumps = 2;                 // 1 ground jump + 1 air (double) jump
    [SerializeField] private float groundCheckDistance = 0.15f;

    private Rigidbody2D rigid;
    private int jumpsUsed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SC_Floor.OnFloorCollision += RefreshJumps;
    }

    private void OnDisable()
    {
        SC_Floor.OnFloorCollision -= RefreshJumps;
    }

    private void Update()
    {
        if (JumpPressed() && CanJump())
            Jump();
    }

    /// <summary>Called when the player lands on a floor — makes all jumps available again.</summary>
    private void RefreshJumps()
    {
        jumpsUsed = 0;
    }

    private bool JumpPressed()
    {
        return Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
    }

    private bool CanJump()
    {
        bool onGround = jumpsUsed == 0;
        bool canDoubleJump = rigid.IsInAir(groundCheckDistance) && jumpsUsed < maxJumps;
        return onGround || canDoubleJump;
    }

    private void Jump()
    {
        rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, 0f); // reset any fall for a consistent height
        rigid.AddForce(new Vector2(0f, jumpSpeed));
        jumpsUsed++;
    }
}
