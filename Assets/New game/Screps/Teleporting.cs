using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            CheckpointController.TeleportPlayer(destination.position);
        }
    }
}
