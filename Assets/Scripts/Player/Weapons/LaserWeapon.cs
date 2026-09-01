using UnityEngine;

/// <summary>
/// Fires laser bolts straight up. Pulls a bolt from the laser pool (auto-located at
/// runtime via <see cref="ProjectilePool.FindFor{T}"/>), positions it at the weapon's
/// muzzle and fires it — the bolt handles its own upward travel, damage and
/// return-to-pool. Implements <see cref="IWeaponInfo"/> so it shows in the weapon
/// selector. Single responsibility: firing the laser.
/// </summary>
public class LaserWeapon : MonoBehaviour, IWeapon, IWeaponInfo
{
    [SerializeField] private string displayName = "Laser";
    [SerializeField] private Sprite icon;

    private ProjectilePool _pool;

    public string DisplayName => displayName;
    public Sprite Icon => icon;

    private void Awake()
    {
        _pool = ProjectilePool.FindFor<LaserProjectile>();
    }

    public void Attack()
    {
        if (_pool == null)
        {
            Debug.LogWarning("LaserWeapon: no laser pool found in the scene.");
            return;
        }

        BaseProjectile laser = _pool.Get();
        if (laser == null)
            return; // pool exhausted this frame

        laser.transform.position = transform.position;
        Debug.Log("Laser fired");
        laser.Fire(); // LaserProjectile ignores the aim and travels straight up
    }
}
