using UnityEngine;

public class TeleportingScre : MonoBehaviour
{
    public Transform destination;

    public bool shwoSnow;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            CheckpointController.TeleportPlayer(destination.position, shwoSnow);
        }
    }
}
