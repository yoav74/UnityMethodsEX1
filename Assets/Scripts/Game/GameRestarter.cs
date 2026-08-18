using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// MVC **Controller** for the restart. Reloads the active scene when the model
/// (<see cref="PlayerLives"/>) raises <see cref="PlayerLives.OnAllLivesLost"/> —
/// reloading resets every object to its initial state, so no bespoke reset logic is
/// needed. It finds the player at runtime via <see cref="PlayerLocator"/> (by
/// "Player" tag) rather than a wired reference, so it keeps working across reloads.
/// </summary>
public class GameRestarter : MonoBehaviour
{
    private PlayerLives lives;

    private void Awake()
    {
        lives = PlayerLocator.FindComponent<PlayerLives>();
    }

    private void OnEnable()
    {
        if (lives == null)
            lives = PlayerLocator.FindComponent<PlayerLives>();

        if (lives != null)
            lives.OnAllLivesLost += RestartGame;
    }

    private void OnDisable()
    {
        if (lives != null)
            lives.OnAllLivesLost -= RestartGame;
    }

    private void RestartGame()
    {
        Scene active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.buildIndex);
    }
}
