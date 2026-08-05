using UnityEngine;

/// <summary>
/// Locates the active player — the single GameObject tagged "Player" — and its
/// components at runtime, so scene objects such as HUD views don't hold a wired
/// reference to a specific player instance. Exactly one active object should carry
/// the "Player" tag per level; disabled players are ignored automatically because
/// <see cref="GameObject.FindGameObjectWithTag"/> only returns active objects.
/// </summary>
public static class PlayerLocator
{
    private const string PlayerTag = "Player";

    public static GameObject Find()
    {
        return GameObject.FindGameObjectWithTag(PlayerTag);
    }

    /// <summary>
    /// Finds a component of type <typeparamref name="T"/> on the player or any of
    /// its children (including inactive ones). Returns null if there is no active player.
    /// </summary>
    public static T FindComponent<T>() where T : class
    {
        GameObject player = Find();
        return player != null ? player.GetComponentInChildren<T>(true) : null;
    }
}
