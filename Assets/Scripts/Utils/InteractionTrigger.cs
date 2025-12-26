using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public event System.Action TriggerEnter;

    private void Awake()
    {
        if (!TryGetComponent<Collider>(out var collider) || !collider.isTrigger)
        {
            Debug.LogWarning("InteractionTrigger requires a Collider with isTrigger set to true.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponentInParent<PlayerController>();
        if (playerController != null)
        {
            TriggerEnter?.Invoke();
        }
    }
}
