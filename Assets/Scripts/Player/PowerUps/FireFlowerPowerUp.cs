using UnityEngine;

public class FireFlowerPowerUp : IPowerUp
{
    public void ApplyPowerUp(GameObject player)
    {
        if (player == null)
            return;

        // include inactive, in case the weapon objects start disabled
        FireballWeapon fireballWeapon = player.GetComponentInChildren<FireballWeapon>(true);
        if (fireballWeapon == null)
            return;

        // Acquire the fireball so it appears in the weapon selector, then equip it.
        WeaponsHandler weaponsHandler = player.GetComponentInChildren<WeaponsHandler>(true);
        if (weaponsHandler != null)
            weaponsHandler.AddWeapon(fireballWeapon);

        fireballWeapon.Equip();
    }
}
