using UnityEngine;

/// <summary>
/// Gives the player their starting weapon (the axe) when a level begins, by adding
/// it to the player's <see cref="WeaponsHandler"/>. Finds the active player at
/// runtime via <see cref="PlayerLocator"/> (by "Player" tag), so it needs no wired
/// reference. Other weapons (e.g. the fireball) are acquired through pickups.
/// </summary>
public class PlayerLoadout : MonoBehaviour
{
    [Tooltip("TEMP (for testing before the Laser pickup exists): also grant the laser at start.")]
    [SerializeField] private bool grantLaserOnStart;

    private void Start()
    {
        GameObject player = PlayerLocator.Find();
        if (player == null)
            return;

        WeaponsHandler handler = player.GetComponentInChildren<WeaponsHandler>(true);
        if (handler == null)
            return;

        AxeWeapon axe = player.GetComponentInChildren<AxeWeapon>(true);
        if (axe != null)
            handler.AddWeapon(axe);

        if (grantLaserOnStart)
        {
            LaserWeapon laser = player.GetComponentInChildren<LaserWeapon>(true);
            if (laser != null)
                handler.AddWeapon(laser);
        }
    }
}
