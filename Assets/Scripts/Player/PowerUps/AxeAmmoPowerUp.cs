using UnityEngine;

/// <summary>
/// Effect applied when the player collects an axe pickup: adds axes to the
/// player's <see cref="AxeAmmo"/> pool. Implements <see cref="IPowerUp"/> so it
/// plugs into the existing <see cref="PlayerPowerUp"/> collection flow without
/// changing it (Open/Closed).
/// </summary>
public class AxeAmmoPowerUp : IPowerUp
{
    private readonly int amount;

    public AxeAmmoPowerUp(int amount)
    {
        this.amount = amount;
    }

    public void ApplyPowerUp(GameObject player)
    {
        if (player == null)
            return;

        AxeAmmo ammo = player.GetComponentInChildren<AxeAmmo>();
        if (ammo != null)
            ammo.Add(amount);
    }
}
