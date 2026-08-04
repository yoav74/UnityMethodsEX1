using System;
using UnityEngine;

/// <summary>
/// A collectible coin. Single responsibility: detect that the player collected it
/// and announce the collection (with a value). It does NOT know how coins are
/// counted or displayed.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class SC_Coin : MonoBehaviour
{
    [SerializeField] private int value = 1;
    [SerializeField] private string playerTag = "Player";

    /// <summary>Raised when any coin is collected, carrying the coin's value.</summary>
    public static event Action<int> OnCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        OnCollected?.Invoke(value);
        gameObject.SetActive(false);
    }
}
