using System;
using UnityEngine;

/// <summary>
/// Model of the player's remaining lives. Single responsibility: track the life
/// count and announce when it changes or reaches zero. It does not respawn the
/// player or reload the scene — those are separate reactors (PlayerDeath /
/// GameRestarter) that subscribe to its events.
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
