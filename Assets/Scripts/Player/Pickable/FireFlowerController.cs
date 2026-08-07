using UnityEngine;

/// <summary>
/// A world pickup that grants the fireball power-up. Single responsibility: detect
/// the player and hand off the effect through the existing PlayerPowerUp flow,
/// then remove itself.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class FireFlowerController : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag(playerTag))
            return;

        PlayerPowerUp playerPowerUp = col.GetComponent<PlayerPowerUp>();
        if (playerPowerUp != null)
            playerPowerUp.CollectPowerUp(new FireFlowerPowerUp());

        gameObject.SetActive(false);
    }
}
