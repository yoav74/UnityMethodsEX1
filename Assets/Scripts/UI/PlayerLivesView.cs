using TMPro;
using UnityEngine;

/// <summary>
/// HUD view for the player's remaining lives. Single responsibility: display the
/// life count. It finds the player at runtime via <see cref="PlayerLocator"/>
/// (by "Player" tag) rather than a wired reference, then depends on
/// <see cref="PlayerLives.OnLivesChanged"/>.
/// </summary>
public class PlayerLivesView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private string format = "Lives: {0}";

    private PlayerLives playerLives;

    private void Awake()
    {
        playerLives = PlayerLocator.FindComponent<PlayerLives>();
    }

    private void Start()
    {
        // Runs after every Awake, so PlayerLives.Lives is set by now.
        if (playerLives != null)
            Render(playerLives.Lives);
    }

    private void OnEnable()
    {
        if (playerLives == null)
            playerLives = PlayerLocator.FindComponent<PlayerLives>();

        if (playerLives == null)
            return;

        playerLives.OnLivesChanged += Render;
        Render(playerLives.Lives);
    }

    private void OnDisable()
    {
        if (playerLives != null)
            playerLives.OnLivesChanged -= Render;
    }

    private void Render(int lives)
    {
        if (label != null)
            label.text = string.Format(format, lives);
    }
}
