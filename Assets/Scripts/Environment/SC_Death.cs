using UnityEngine;

/// <summary>
/// A spike hazard. Raises a static event when the player touches it; PlayerDeath
/// reacts to that to cost a life and respawn.
/// </summary>
public class SC_Death : MonoBehaviour
{
    public delegate void SpikeCollisionHandler();
    public static event SpikeCollisionHandler OnSpikeCollision;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            OnSpikeCollision?.Invoke();
    }
}
