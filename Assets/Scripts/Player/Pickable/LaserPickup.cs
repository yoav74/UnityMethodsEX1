using UnityEngine;

/// <summary>
/// A world pickup that grants the laser power-up. Single responsibility: detect the
/// player and hand off the effect through the existing PlayerPowerUp flow, then remove
/// itself. Mirrors <see cref="FireFlowerController"/>.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class LaserPickup : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(playerTag))
            return;

        PlayerPowerUp playerPowerUp = col.GetComponent<PlayerPowerUp>();
        if (playerPowerUp != null)
            playerPowerUp.CollectPowerUp(new LaserPowerUp());

        gameObject.SetActive(false);
    }
}
