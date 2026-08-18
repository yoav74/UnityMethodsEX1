using System.Collections;
using UnityEngine;

/// <summary>
/// A tile that blinks in and out on a timer: solid and visible for
/// <see cref="visibleTime"/> seconds, then hidden and intangible for
/// <see cref="hiddenTime"/> seconds, repeating. Single responsibility: drive its own
/// on/off cycle by toggling its renderer and collider so the player falls through
/// while it is gone.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class BlinkingTile : MonoBehaviour
{
    [SerializeField] private float visibleTime = 2f;
    [SerializeField] private float hiddenTime = 2f;

    private SpriteRenderer sprite;
    private Collider2D tileCollider;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        tileCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            SetShown(true);
            yield return new WaitForSeconds(visibleTime);

            SetShown(false);
            yield return new WaitForSeconds(hiddenTime);
        }
    }

    private void SetShown(bool shown)
    {
        sprite.enabled = shown;
        tileCollider.enabled = shown; // off = player falls through
    }
}
