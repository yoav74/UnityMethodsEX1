using UnityEngine;

/// <summary>
/// A world pickup (lightning) that gives the player a temporary speed boost.
/// Single responsibility: detect the player and hand off the boost effect through
/// the existing <see cref="PlayerPowerUp"/> flow, then remove itself.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class LightningPickup : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.5f; // +50% of normal speed
    [SerializeField] private float duration = 5f;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        PlayerPowerUp playerPowerUp = other.GetComponent<PlayerPowerUp>();
        if (playerPowerUp != null)
            playerPowerUp.CollectPowerUp(new SpeedBoostPowerUp(speedMultiplier, duration));

        gameObject.SetActive(false);
    }
}
