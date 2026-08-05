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

    public int Lives { get; private set; }

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
        if (Lives <= 0)
            return;

        Lives--;
        OnLivesChanged?.Invoke(Lives);

        if (Lives <= 0)
            OnAllLivesLost?.Invoke();
    }
}
