using System;
using UnityEngine;

/// <summary>
/// An enemy's health. Single responsibility: track hit points, take damage, and
/// die when they run out. Defaults to 1 HP so it "dies if shot" in one hit, but
/// tougher enemies just raise the value.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 1;

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
        Destroy(gameObject);
    }
}
