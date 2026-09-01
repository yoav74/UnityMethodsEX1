using UnityEngine;

public class FireballWeapon : MonoBehaviour, IUseableWeapon, IWeaponInfo
{
    public GameObject projectile;
    [SerializeField] private string displayName = "Fireball";
    [SerializeField] private Sprite icon;

    private bool _isEquip = false;

    public string DisplayName => displayName;
    public Sprite Icon => icon;

    public void Attack()
    {
        if (projectile != null && _isEquip)
        {
            GameObject curProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            ProjectileFireball scProjectile =  curProjectile.GetComponent<ProjectileFireball>();
            if(scProjectile != null)
            {
                float direction = 1;
                if(transform.parent != null)
                    direction = transform.parent.localScale.x;
                scProjectile.Fire(new Vector2(direction, 0f));
            }
        }
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
