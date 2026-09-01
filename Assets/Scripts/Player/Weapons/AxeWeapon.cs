using UnityEngine;

/// <summary>
/// Throws axe projectiles, consuming one axe from the player's <see cref="AxeAmmo"/>
/// pool per shot. Single responsibility: firing. It does not track the ammo count
/// itself (that is <see cref="AxeAmmo"/>) — it only asks to consume one.
/// </summary>
public class AxeWeapon : MonoBehaviour, IWeapon, IWeaponInfo
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private AxeAmmo ammo;
    [SerializeField] private string displayName = "Axe";
    [SerializeField] private Sprite icon;

    public string DisplayName => displayName;
    public Sprite Icon => icon;

    private void Awake()
    {
        if (ammo == null)
            ammo = GetComponentInParent<AxeAmmo>();
    }

    public void Attack()
    {
        if (projectile == null || ammo == null || !ammo.TryConsume(1))
            return;

        GameObject curProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        ProjectileAxe scProjectile = curProjectile.GetComponent<ProjectileAxe>();
        if (scProjectile != null)
        {
            float direction = transform.parent != null ? transform.parent.localScale.x : 1f;
            scProjectile.Fire(new Vector2(direction, 0f));
        }
    }
}
