using UnityEngine;

/// <summary>
/// Handles the player dying (currently from spikes): costs a life and respawns the
/// player at its start position while lives remain. When the last life is lost it
/// does not respawn — <see cref="GameRestarter"/> reacts to that instead.
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
        SC_Death.OnSpikeCollision += OnSpikeCollision;
    }

    private void OnDisable()
    {
        SC_Death.OnSpikeCollision -= OnSpikeCollision;
    }

    private void OnSpikeCollision()
    {
        if (lives != null)
            lives.LoseLife();

        // Respawn only while the player still has lives (the last death restarts instead).
        if (lives == null || lives.Lives > 0)
            transform.position = startPosition;
    }
}
