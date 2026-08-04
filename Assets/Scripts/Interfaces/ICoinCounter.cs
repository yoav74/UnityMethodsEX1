using System;

/// <summary>
/// Read-only view of the coin count. Lets UI (or any consumer) depend on an
/// abstraction instead of the concrete manager (Dependency Inversion).
/// </summary>
public interface ICoinCounter
{
    int Count { get; }

    /// <summary>Raised whenever <see cref="Count"/> changes, passing the new total.</summary>
    event Action<int> OnCoinsChanged;
}
