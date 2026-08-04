using System;
using UnityEngine;

/// <summary>
/// The coin "model": single responsibility is to track how many coins have been
/// collected and notify listeners when the total changes. It knows nothing about
/// UI. Any consumer (HUD, extra-life logic, audio) subscribes to
/// <see cref="OnCoinsChanged"/> without this class needing to change (Open/Closed).
/// </summary>
public class SC_CoinsManager : MonoBehaviour, ICoinCounter
{
    public int Count { get; private set; }

    public event Action<int> OnCoinsChanged;

    private void OnEnable()
    {
        SC_Coin.OnCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        SC_Coin.OnCollected -= HandleCoinCollected;
    }

    private void HandleCoinCollected(int value)
    {
        Count += value;
        OnCoinsChanged?.Invoke(Count);
    }
}
