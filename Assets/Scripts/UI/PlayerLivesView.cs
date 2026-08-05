using TMPro;
using UnityEngine;

/// <summary>
/// HUD view for the player's remaining lives. Single responsibility: display the
/// life count. It depends on <see cref="PlayerLives"/> through its
/// <see cref="PlayerLives.OnLivesChanged"/> event and an inspector-injected TMP
/// label (Dependency Inversion; no GameObject.Find).
/// </summary>
public class PlayerLivesView : MonoBehaviour
{
    [SerializeField] private PlayerLives playerLives;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private string format = "Lives: {0}";

    private void Start()
    {
        if (playerLives)
        {
            Render(playerLives.Lives);
        }
    }

    private void OnEnable()
    {
        if (playerLives == null)
            return;

        playerLives.OnLivesChanged += Render;
        Render(playerLives.Lives); // show the correct value immediately
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
