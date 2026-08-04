using UnityEngine;

/// <summary>
/// Display metadata for a weapon shown in the weapon selector. Kept separate from
/// <see cref="IWeapon"/> (behavior) so consumers that only need presentation depend
/// on just this (Interface Segregation).
/// </summary>
public interface IWeaponInfo
{
    string DisplayName { get; }
    Sprite Icon { get; }
}
