using UnityEngine;

public class TempInit : MonoBehaviour
{
    public WeaponsHandler weaponsHandler;

    public FireballWeapon fireballWeapon;

    public AxeWeapon axeWeapon;

    void Start()
    {
        if(weaponsHandler != null)
        {
            // Player starts with the axe; the fireball is acquired via the FireFlower pickup.
            if(axeWeapon != null)
                weaponsHandler.AddWeapon(axeWeapon);
        }
    }
}
