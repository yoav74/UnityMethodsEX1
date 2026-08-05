using UnityEngine;

/// <summary>
/// Kills the player when they touch this object (e.g. an enemy body). Single
/// responsibility: turn contact with the player into a call to
/// <see cref="PlayerDeath.Die"/> — the same death path spikes use, so the player
/// loses a life and respawns. The life loss respects its invulnerability window,
/// so overlapping contacts only cost one life.
/// </summary>
public class DamagePlayerOnContact : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision) => TryDamage(collision.collider);

    private void OnTriggerEnter2D(Collider2D other) => TryDamage(other);

    private void TryDamage(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        PlayerDeath death = other.GetComponentInParent<PlayerDeath>();
        if (death != null)
            death.Die();
    }
}
