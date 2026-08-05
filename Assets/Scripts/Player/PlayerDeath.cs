using UnityEngine;

/// <summary>
/// Handles the player dying: costs a life and respawns the player at its start
/// position while lives remain. When the last life is lost it does not respawn —
/// <see cref="GameRestarter"/> reacts to that instead. Both spikes and enemies
/// route through <see cref="Die"/>, so every death is handled the same way.
/// </summary>
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerLives lives;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
        if (lives == null)
            lives = GetComponent<PlayerLives>();
    }

    private void OnEnable()
    {
        SC_Death.OnSpikeCollision += Die;
    }

    private void OnDisable()
    {
        SC_Death.OnSpikeCollision -= Die;
    }

    /// <summary>
    /// Kills the player once: costs a life (respecting invulnerability) and, while
    /// lives remain, respawns at the start position.
    /// </summary>
    public void Die()
    {
        if (lives != null)
            lives.LoseLife();

        // Respawn only while the player still has lives (the last death restarts instead).
        if (lives == null || lives.Lives > 0)
            transform.position = startPosition;
    }
}
