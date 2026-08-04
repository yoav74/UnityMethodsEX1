using System;
using UnityEngine;

/// <summary>
/// Holds how many axes the player currently has. Single responsibility: track the
/// axe ammo count and notify listeners when it changes. It knows nothing about
/// firing, pickups, or UI — those consume this via <see cref="Add"/>,
/// <see cref="TryConsume"/> and <see cref="IAmmoCounter"/>.
/// </summary>
public class AxeAmmo : MonoBehaviour, IAmmoCounter
{
    [SerializeField] private int startingCount = 0;

    public int Count { get; private set; }

    public event Action<int> OnAmmoChanged;

    private void Start()
    {
        SetCount(startingCount);
    }

    /// <summary>Adds ammo, e.g. when an axe pickup is collected.</summary>
    public void Add(int amount)
    {
        if (amount <= 0)
            return;

        SetCount(Count + amount);
    }

    /// <summary>Consumes ammo only if enough is available. Returns true on success.</summary>
    public bool TryConsume(int amount)
    {
        if (amount <= 0 || Count < amount)
            return false;

        SetCount(Count - amount);
        return true;
    }

    private void SetCount(int value)
    {
        Count = Mathf.Max(0, value);
        OnAmmoChanged?.Invoke(Count);
    }
}
