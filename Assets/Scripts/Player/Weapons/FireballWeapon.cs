using UnityEngine;

/// <summary>
/// Fires fireball projectiles horizontally in the direction the player faces. Prefers an
/// assigned <see cref="ProjectilePool"/> (reuse), falling back to Instantiate when no pool
/// is set. Single responsibility: firing the fireball.
/// </summary>
public class FireballWeapon : MonoBehaviour, IUseableWeapon, IWeaponInfo
{
    [SerializeField] private GameObject projectile;   // used only when no pool is assigned
    [SerializeField] private ProjectilePool pool;     // preferred: reuse instead of Instantiate
    [SerializeField] private string displayName = "Fireball";
    [SerializeField] private Sprite icon;

    private bool _isEquip = false;

    public string DisplayName => displayName;
    public Sprite Icon => icon;

    private void Awake()
    {
        if (pool == null)
            pool = ProjectilePool.FindFor<ProjectileFireball>();
    }

    public void Attack()
    {
        if (!_isEquip)
            return;

        ProjectileFireball fireball = Spawn();
        if (fireball == null)
            return;

        float direction = transform.parent != null ? transform.parent.localScale.x : 1f;
        fireball.Fire(new Vector2(direction, 0f));
    }

    private ProjectileFireball Spawn()
    {
        if (pool != null)
        {
            BaseProjectile pooled = pool.Get();
            if (pooled != null)
                pooled.transform.position = transform.position;
            return pooled as ProjectileFireball;
        }

        if (projectile == null)
            return null;

        GameObject instance = Instantiate(projectile, transform.position, Quaternion.identity);
        return instance.GetComponent<ProjectileFireball>();
    }

    public void Equip()
    {
        _isEquip = true;
    }

    public void UnEquip()
    {
        _isEquip = false;
    }
}
