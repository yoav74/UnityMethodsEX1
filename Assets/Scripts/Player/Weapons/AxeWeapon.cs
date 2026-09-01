using UnityEngine;

/// <summary>
/// Throws axe projectiles, consuming one axe from the player's <see cref="AxeAmmo"/>
/// pool per shot. Prefers an assigned <see cref="ProjectilePool"/> (reuse), falling back
/// to Instantiate when no pool is set. Single responsibility: firing. It does not track
/// the ammo count itself (that is <see cref="AxeAmmo"/>) — it only asks to consume one.
/// </summary>
public class AxeWeapon : MonoBehaviour, IWeapon, IWeaponInfo
{
    [SerializeField] private GameObject projectile;   // used only when no pool is assigned
    [SerializeField] private ProjectilePool pool;     // preferred: reuse instead of Instantiate
    [SerializeField] private AxeAmmo ammo;
    [SerializeField] private string displayName = "Axe";
    [SerializeField] private Sprite icon;

    public string DisplayName => displayName;
    public Sprite Icon => icon;

    private void Awake()
    {
        if (ammo == null)
            ammo = GetComponentInParent<AxeAmmo>();

        if (pool == null)
            pool = ProjectilePool.FindFor<ProjectileAxe>();
    }

    public void Attack()
    {
        bool canSpawn = pool != null || projectile != null;
        if (!canSpawn || ammo == null || !ammo.TryConsume(1))
            return;

        ProjectileAxe axe = Spawn();
        if (axe == null)
            return;

        float direction = transform.parent != null ? transform.parent.localScale.x : 1f;
        axe.Fire(new Vector2(direction, 0f));
    }

    private ProjectileAxe Spawn()
    {
        if (pool != null)
        {
            BaseProjectile pooled = pool.Get();
            if (pooled != null)
                pooled.transform.position = transform.position;
            return pooled as ProjectileAxe;
        }

        GameObject instance = Instantiate(projectile, transform.position, Quaternion.identity);
        return instance.GetComponent<ProjectileAxe>();
    }
}
