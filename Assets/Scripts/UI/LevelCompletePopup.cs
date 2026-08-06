using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Shows a "Level Complete" popup when <see cref="LevelExitDoor.OnLevelComplete"/>
/// fires, and restarts the level when its button is pressed. Single responsibility:
/// present level completion. Starts hidden and (optionally) pauses the game while shown.
/// </summary>
public class LevelCompletePopup : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button restartButton;
    [SerializeField] private bool pauseOnComplete = true;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);
    }

    private void OnEnable()
    {
        LevelExitDoor.OnLevelComplete += Show;
    }

    private void OnDisable()
    {
        LevelExitDoor.OnLevelComplete -= Show;
    }

    private void Show()
    {
        if (panel != null)
            panel.SetActive(true);

        if (pauseOnComplete)
            Time.timeScale = 0f; // UI buttons still work while paused
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f; // always restore before reloading
        Scene active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.buildIndex);
    }
}
