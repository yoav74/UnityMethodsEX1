using System;
using UnityEngine;

/// <summary>
/// The player's key inventory. Single responsibility: track how many keys the
/// player is carrying. Pickups add keys via <see cref="AddKey"/>; the finish door
/// consumes one via <see cref="TryUseKey"/>. It does not know about doors or UI.
/// </summary>
public class PlayerKeys : MonoBehaviour
{
    public int Count { get; private set; }

    public bool HasKey => Count > 0;

    /// <summary>Raised whenever the key count changes, passing the new total.</summary>
    public event Action<int> OnKeysChanged;

    public void AddKey(int amount = 1)
    {
        if (amount <= 0)
            return;

        Count += amount;
        OnKeysChanged?.Invoke(Count);
    }

    /// <summary>Consumes one key if any are held. Returns true when a key was used.</summary>
    public bool TryUseKey()
    {
        if (Count <= 0)
            return false;

        Count--;
        OnKeysChanged?.Invoke(Count);
        return true;
    }
}
