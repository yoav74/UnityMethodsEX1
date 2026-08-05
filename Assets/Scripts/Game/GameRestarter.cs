using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Restarts the game when the player runs out of lives. Single responsibility:
/// reload the active scene in response to <see cref="PlayerLives.OnAllLivesLost"/>.
/// Reloading gives every object a fresh start (lives reset to their starting value).
/// </summary>
public class GameRestarter : MonoBehaviour
{
    [SerializeField] private PlayerLives lives;

    private void OnEnable()
    {
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
