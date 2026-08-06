using UnityEngine;

/// <summary>
/// A world pickup that grants the player a key. Single responsibility: detect the
/// player and hand off a key effect through the existing <see cref="PlayerPowerUp"/>
/// flow, then remove itself.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class KeyPickup : MonoBehaviour
{
    [SerializeField] private int amount = 1;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        PlayerPowerUp playerPowerUp = other.GetComponent<PlayerPowerUp>();
        if (playerPowerUp != null)
            playerPowerUp.CollectPowerUp(new KeyPowerUp(amount));

        gameObject.SetActive(false);
    }
}
