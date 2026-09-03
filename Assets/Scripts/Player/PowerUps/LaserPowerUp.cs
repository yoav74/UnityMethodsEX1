using UnityEngine;

/// <summary>
/// Grants Mario the laser: finds the player's <see cref="LaserWeapon"/> and adds it to
/// the <see cref="WeaponsHandler"/> so it shows in the selector and can be fired. This is
/// the gate — the laser is only in the inventory once this power-up is collected.
/// Mirrors <see cref="FireFlowerPowerUp"/>.
/// </summary>
public class LaserPowerUp : IPowerUp
{
    public void ApplyPowerUp(GameObject player)
    {
        if (player == null)
            return;

        // include inactive, in case the weapon object starts disabled
        LaserWeapon laserWeapon = player.GetComponentInChildren<LaserWeapon>(true);
        if (laserWeapon == null)
            return;

        WeaponsHandler weaponsHandler = player.GetComponentInChildren<WeaponsHandler>(true);
        if (weaponsHandler != null)
            weaponsHandler.AddWeapon(laserWeapon);

        Debug.Log("Laser power-up collected");
    }
}
