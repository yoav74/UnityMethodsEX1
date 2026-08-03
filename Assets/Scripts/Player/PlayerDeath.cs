using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
     private Vector3 startPositon;

    private void OnEnable()
    {
        SC_Death.OnSpikeCollision += OnSpikeCollision;
    }

    private void OnDisable()
    {
        SC_Death.OnSpikeCollision -= OnSpikeCollision;
    }
    void Awake()
    {
        startPositon = transform.position;
    }

    private void OnSpikeCollision()
    {
        transform.position = startPositon;
    }
}
