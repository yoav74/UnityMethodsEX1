using System;
using UnityEngine;

/// <summary>
/// An enemy's health. Single responsibility: track hit points, take damage, and
/// die when they run out. Defaults to 1 HP so it "dies if shot" in one hit, but
/// tougher enemies just raise the value. Raises <see cref="OnDied"/> before it is
/// destroyed (used later by the ME-10 spawner).
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 1;

    /// <summary>Raised just before the enemy is destroyed.</summary>
    public event Action OnDied;

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || health <= 0)
            return;

        health -= amount;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        OnDied?.Invoke();
        Destroy(gameObject);
    }
}
