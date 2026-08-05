/// <summary>
/// Something that can take damage (e.g. an enemy). Lets weapons and projectiles
/// damage targets through an abstraction instead of concrete types, so new
/// damageable types work with existing weapons unchanged (Dependency Inversion / OCP).
/// </summary>
public interface IDamageable
{
    void TakeDamage(int amount);
}
