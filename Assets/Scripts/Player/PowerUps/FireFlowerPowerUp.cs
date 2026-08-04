using UnityEngine;

public class FireFlowerPowerUp : IPowerUp
{
    public void ApplyPowerUp(GameObject player)
    {
        if(player == null)
            return;

        FireballWeapon fireballWeapon = player.GetComponentInChildren<FireballWeapon>();
        if(fireballWeapon == null)
            return;

        // Acquire the fireball so it appears in the weapon selector, then equip it.
        WeaponsHandler weaponsHandler = player.GetComponentInChildren<WeaponsHandler>();
        if(weaponsHandler != null)
            weaponsHandler.AddWeapon(fireballWeapon);

        fireballWeapon.Equip();
    }
}
