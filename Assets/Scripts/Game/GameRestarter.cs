using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Restarts the game when the player runs out of lives. Single responsibility:
/// reload the active scene in response to <see cref="PlayerLives.OnAllLivesLost"/>.
/// It finds the player at runtime via <see cref="PlayerLocator"/> (by "Player" tag)
/// rather than a wired reference, so it keeps working across scene reloads.
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
