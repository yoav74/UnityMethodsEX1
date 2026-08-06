using System;
using UnityEngine;

/// <summary>
/// The level's finish door. Single responsibility: when the player reaches it with
/// a key, consume the key and announce that the level is complete. It does not show
/// UI or reload the scene — a listener (see LevelCompletePopup) reacts to
/// <see cref="OnLevelComplete"/>. Without a key the door stays locked.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class LevelExitDoor : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    /// <summary>Raised when the player unlocks and reaches the finish door.</summary>
    public static event Action OnLevelComplete;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        PlayerKeys keys = other.GetComponentInParent<PlayerKeys>();
        if (keys == null || !keys.TryUseKey())
            return; // locked: the player has no key

        OnLevelComplete?.Invoke();
    }
}
