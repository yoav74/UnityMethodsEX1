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
            if(fireballWeapon != null)
                weaponsHandler.AddWeapon(fireballWeapon);
            if(axeWeapon != null)
                weaponsHandler.AddWeapon(axeWeapon);
        }
    }
}
