using UnityEngine;

/// <summary>
/// <b>Builder</b> for laser bolts: assembles a <see cref="LaserProjectile"/> one step at
/// a time (speed, lifetime, size, sprite) and produces the finished instance from a
/// supplied prefab. This separates *how* a laser is put together from the code that just
/// wants a ready laser — callers use the <see cref="LaserDirector"/> or the factory
/// rather than knowing these steps. (Damage is a flat 1 for every projectile, so it is
/// not a build step.)
/// </summary>
public class LaserBuilder
{
    private readonly GameObject _prefab;

    private float _speed = 12f;
    private float _lifetime = 3f;
    private float _size = 1f;
    private Sprite _sprite;

    /// <param name="prefab">A prefab with a <see cref="LaserProjectile"/> (plus its
    /// Rigidbody2D / Collider2D / SpriteRenderer). The built bolt keeps the prefab's
    /// physics and collider setup; the steps below override its configurable values.</param>
    public LaserBuilder(GameObject prefab)
    {
        _prefab = prefab;
    }

    public LaserBuilder SetSpeed(float speed)
    {
        _speed = speed;
        return this;
    }

    public LaserBuilder SetLifetime(float lifetime)
    {
        _lifetime = lifetime;
        return this;
    }

    public LaserBuilder SetSize(float size)
    {
        _size = size;
        return this;
    }

    public LaserBuilder SetSprite(Sprite sprite)
    {
        _sprite = sprite;
        return this;
    }

    /// <summary>Instantiates the prefab and applies the configured steps to it.</summary>
    public LaserProjectile Build()
    {
        if (_prefab == null)
        {
            Debug.LogError("LaserBuilder: no laser prefab was provided.");
            return null;
        }

        GameObject instance = Object.Instantiate(_prefab);

        LaserProjectile laser = instance.GetComponent<LaserProjectile>();
        if (laser == null)
        {
            Debug.LogError("LaserBuilder: prefab has no LaserProjectile component.");
            Object.Destroy(instance);
            return null;
        }

        laser.Configure(_speed, _lifetime);
        instance.transform.localScale = Vector3.one * _size;

        if (_sprite != null)
        {
            SpriteRenderer renderer = instance.GetComponentInChildren<SpriteRenderer>();
            if (renderer != null)
                renderer.sprite = _sprite;
        }

        return laser;
    }
}
