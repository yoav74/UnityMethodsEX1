using System;
using UnityEngine;

/// <summary>
/// MVC **Model** for the player's health / lives. Single responsibility: hold the
/// count and its rules (max cap, invulnerability window) and announce changes via
/// events — it has no UI and reads no input.
///
/// The health feature follows MVC:
///   • Model      — this class (<see cref="PlayerLives"/>)
///   • View       — <see cref="PlayerLivesView"/> (renders the count)
///   • Controllers— <see cref="PlayerDeath"/> (damage), the life pickups (heal),
///                  and <see cref="GameRestarter"/> (restart on 0)
/// </summary>
public class PlayerLives : MonoBehaviour
{
    [SerializeField] private int startingLives = 3;
    [SerializeField] private int maxLives = 0; // 0 or less = no cap
    [SerializeField] private float invulnerabilityDuration = 0.75f;

    private float invulnerableUntil;

    public int Lives { get; private set; }

    /// <summary>True during the brief mercy window after losing a life.</summary>
    public bool IsInvulnerable => Time.time < invulnerableUntil;

    /// <summary>Raised whenever the life count changes, passing the new total.</summary>
    public event Action<int> OnLivesChanged;

    /// <summary>Raised when the last life is lost.</summary>
    public event Action OnAllLivesLost;

    private void Awake()
    {
        Lives = startingLives;
    }

    public void LoseLife()
    {
        // Ignore repeated/simultaneous hits during the invulnerability window so a
        // single death (e.g. landing across two spike colliders) only costs one life.
        if (Lives <= 0 || IsInvulnerable)
            return;

        invulnerableUntil = Time.time + invulnerabilityDuration;

        Lives--;
        OnLivesChanged?.Invoke(Lives);

        if (Lives <= 0)
            OnAllLivesLost?.Invoke();
    }

    /// <summary>Adds lives, e.g. when a life pickup is collected (respects the cap, if any).</summary>
    public void AddLife(int amount = 1)
    {
        if (amount <= 0)
            return;

        int newValue = Lives + amount;
        if (maxLives > 0)
            newValue = Mathf.Min(newValue, maxLives);

        if (newValue == Lives)
            return;

        Lives = newValue;
        OnLivesChanged?.Invoke(Lives);
    }
}
