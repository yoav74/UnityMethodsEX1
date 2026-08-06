using UnityEngine;

/// <summary>
/// A projectile fired by an enemy. Single responsibility: kill the player on
/// contact (through the shared <see cref="PlayerDeath.Die"/> path, so it respects
/// the invulnerability window) and clean itself up. It expires after
/// <see cref="lifetime"/> seconds so stray shots don't linger.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private string playerTag = "Player";

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        PlayerDeath death = other.GetComponentInParent<PlayerDeath>();
        if (death != null)
            death.Die();

        Destroy(gameObject);
    }
}
