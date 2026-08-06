using UnityEngine;

/// <summary>
/// Effect applied when the player collects a key pickup: adds keys to the player's
/// <see cref="PlayerKeys"/>. Implements <see cref="IPowerUp"/> so it plugs into the
/// existing <see cref="PlayerPowerUp"/> collection flow without changing it (Open/Closed).
/// </summary>
public class KeyPowerUp : IPowerUp
{
    private readonly int amount;

    public KeyPowerUp(int amount)
    {
        this.amount = amount;
    }

    public void ApplyPowerUp(GameObject player)
    {
        if (player == null)
            return;

        PlayerKeys keys = player.GetComponentInChildren<PlayerKeys>(true);
        if (keys != null)
            keys.AddKey(amount);
    }
}
