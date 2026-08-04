using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Model of the player's weapon inventory: which weapons have been acquired and
/// which one is currently selected. Single responsibility is to own that state and
/// notify listeners when it changes — it does not read input or draw UI (those are
/// <see cref="WeaponInput"/> and <see cref="WeaponSelectorView"/>).
/// </summary>
public class WeaponsHandler : MonoBehaviour
{
    private readonly List<IWeapon> weapons = new List<IWeapon>();

    /// <summary>Acquired weapons, in acquisition order.</summary>
    public IReadOnlyList<IWeapon> Weapons => weapons;

    public int SelectedIndex { get; private set; } = -1;

    public IWeapon Selected => IsValidIndex(SelectedIndex) ? weapons[SelectedIndex] : null;

    /// <summary>Raised when a weapon is acquired (or the inventory otherwise changes).</summary>
    public event Action OnInventoryChanged;

    /// <summary>Raised when the selected weapon changes, passing the new index.</summary>
    public event Action<int> OnSelectionChanged;

    public void AddWeapon(IWeapon weapon)
    {
        if (weapon == null || weapons.Contains(weapon))
            return;

        weapons.Add(weapon);
        OnInventoryChanged?.Invoke();

        if (SelectedIndex < 0) // auto-select the first weapon acquired
            Select(0);
    }

    public void Select(int index)
    {
        if (!IsValidIndex(index) || index == SelectedIndex)
            return;

        SelectedIndex = index;
        OnSelectionChanged?.Invoke(SelectedIndex);
    }

    public void SelectNext()
    {
        if (weapons.Count > 0)
            Select((SelectedIndex + 1) % weapons.Count);
    }

    public void SelectPrevious()
    {
        if (weapons.Count > 0)
            Select((SelectedIndex - 1 + weapons.Count) % weapons.Count);
    }

    /// <summary>Fires the currently selected weapon, if any.</summary>
    public void FireSelected()
    {
        Selected?.Attack();
    }

    private bool IsValidIndex(int index) => index >= 0 && index < weapons.Count;
}
