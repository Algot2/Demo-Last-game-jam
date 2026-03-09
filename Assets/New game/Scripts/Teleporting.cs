using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public Transform destination;

    public bool shwoSnow;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            CheckpointController.TeleportPlayer(destination.position, shwoSnow);
        }
    }
}
