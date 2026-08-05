using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays the player's acquired weapons as a vertical list of icon + name slots
/// and keeps the selected one highlighted. Single responsibility: build and refresh
/// the slot list from the player's <see cref="WeaponsHandler"/>. It finds the player
/// at runtime via <see cref="PlayerLocator"/> (by "Player" tag) rather than a wired
/// reference; each <see cref="WeaponSlotView"/> renders itself.
/// </summary>
public class WeaponSelectorView : MonoBehaviour
{
    [SerializeField] private WeaponSlotView slotPrefab;
    [SerializeField] private Transform slotContainer;

    private WeaponsHandler handler;
    private readonly List<WeaponSlotView> slots = new List<WeaponSlotView>();

    private void Awake()
    {
        handler = PlayerLocator.FindComponent<WeaponsHandler>();
    }

    private void OnEnable()
    {
        if (handler == null)
            handler = PlayerLocator.FindComponent<WeaponsHandler>();

        if (handler == null)
            return;

        handler.OnInventoryChanged += Rebuild;
        handler.OnSelectionChanged += Highlight;
        Rebuild();
    }

    private void OnDisable()
    {
        if (handler == null)
            return;

        handler.OnInventoryChanged -= Rebuild;
        handler.OnSelectionChanged -= Highlight;
    }

    private void Rebuild()
    {
        if (slotPrefab == null || slotContainer == null)
            return;

        foreach (WeaponSlotView slot in slots)
        {
            if (slot != null)
                Destroy(slot.gameObject);
        }
        slots.Clear();

        IReadOnlyList<IWeapon> weapons = handler.Weapons;
        for (int i = 0; i < weapons.Count; i++)
        {
            WeaponSlotView slot = Instantiate(slotPrefab, slotContainer);
            slot.Bind(GetName(weapons[i]), GetIcon(weapons[i]));
            slots.Add(slot);
        }

        Highlight(handler.SelectedIndex);
    }

    private void Highlight(int selectedIndex)
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].SetSelected(i == selectedIndex);
    }

    private static string GetName(IWeapon weapon)
    {
        if (weapon is IWeaponInfo info && !string.IsNullOrEmpty(info.DisplayName))
            return info.DisplayName;

        return weapon.GetType().Name;
    }

    private static Sprite GetIcon(IWeapon weapon)
    {
        return weapon is IWeaponInfo info ? info.Icon : null;
    }
}
