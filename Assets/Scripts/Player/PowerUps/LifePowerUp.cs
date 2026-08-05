using UnityEngine;

/// <summary>
/// Effect applied when the player collects a life pickup: adds lives to the
/// player's <see cref="PlayerLives"/>. Implements <see cref="IPowerUp"/> so it
/// plugs into the existing <see cref="PlayerPowerUp"/> collection flow without
/// changing it (Open/Closed).
/// </summary>
public class LifePowerUp : IPowerUp
{
    private readonly int amount;

    public LifePowerUp(int amount)
    {
        this.amount = amount;
    }

    public void ApplyPowerUp(GameObject player)
    {
        if (player == null)
            return;

        PlayerLives lives = player.GetComponentInChildren<PlayerLives>();
        if (lives != null)
            lives.AddLife(amount);
    }
}
