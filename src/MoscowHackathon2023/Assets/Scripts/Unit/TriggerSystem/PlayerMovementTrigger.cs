using TriggerSystem;
using UnityEngine;

public class PlayerMovementTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TriggerDetector trigger))
        {
            trigger.Enter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out TriggerDetector trigger))
        {
            trigger.Exit();
        }
    }
}
