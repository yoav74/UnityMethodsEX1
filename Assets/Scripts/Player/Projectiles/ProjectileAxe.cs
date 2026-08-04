using UnityEngine;

public class ProjectileAxe : MonoBehaviour
{
    public float speedX = 5f;
    public float speedY = 5f;
    public float lifetime = 3f;
    [SerializeField] private float rotationSpeed = 720f; // degrees per second

    private Rigidbody2D rb;
    private float spinDirection = -1f;
    private bool thrown;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Attack(float direction)
    {
        if(rb != null)
        {
            transform.localScale = new Vector3(direction, 1, 1);
            spinDirection = -Mathf.Sign(direction); // tumble in the direction of travel
            rb.AddForce(new Vector2(direction * speedX, speedY));
            Destroy(gameObject, lifetime);
            thrown = true;
        }
    }

    void Update()
    {
        if (thrown)
            transform.Rotate(0f, 0f, spinDirection * rotationSpeed * Time.deltaTime);
    }
}
