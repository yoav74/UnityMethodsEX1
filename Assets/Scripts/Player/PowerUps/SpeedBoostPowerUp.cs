using UnityEngine;

/// <summary>
/// Effect applied when the player collects the lightning: gives the player's
/// movement a temporary speed boost (the boost's timing and its sprite tint are
/// handled by <see cref="PlayerMovement"/>). Implements <see cref="IPowerUp"/> so it
/// plugs into the existing <see cref="PlayerPowerUp"/> collection flow (Open/Closed).
/// </summary>
public class SpeedBoostPowerUp : IPowerUp
{
    private readonly float multiplier;
    private readonly float duration;

    public SpeedBoostPowerUp(float multiplier, float duration)
    {
        this.multiplier = multiplier;
        this.duration = duration;
    }

    public void ApplyPowerUp(GameObject player)
    {
        if (player == null)
            return;

        PlayerMovement movement = player.GetComponentInChildren<PlayerMovement>(true);
        if (movement != null)
            movement.ApplySpeedBoost(multiplier, duration);
    }
}
