using UnityEngine;

/// <summary>
/// The axe projectile: thrown in an arc in the direction it is fired and tumbling while
/// in flight. Inherits the shared firing sequence from <see cref="BaseProjectile"/> and
/// customises the facing, the arced launch impulse, and the spin.
/// </summary>
public class ProjectileAxe : BaseProjectile
{
    [SerializeField] private float speedX = 5f;
    [SerializeField] private float speedY = 5f;
    [SerializeField] private float rotationSpeed = 720f; // degrees per second

    private float spinDirection = -1f;
    private bool thrown;

    protected override void Prepare(Vector2 direction)
    {
        base.Prepare(direction);
        float facing = Mathf.Sign(direction.x);
        transform.localScale = new Vector3(facing, 1f, 1f);
        spinDirection = -facing; // tumble in the direction of travel
    }

    protected override void Launch(Vector2 direction)
    {
        if (body != null)
            body.AddForce(new Vector2(Mathf.Sign(direction.x) * speedX, speedY));
    }

    protected override void OnFired(Vector2 direction)
    {
        thrown = true;
    }

    private void Update()
    {
        if (thrown)
            transform.Rotate(0f, 0f, spinDirection * rotationSpeed * Time.deltaTime);
    }
}
