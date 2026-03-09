using UnityEngine;

public class TeleportingScrep : MonoBehaviour
{
    public Transform destination;
    public GameObject[] AnabolDisabol;

    public bool shwoSnow;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            CheckpointController.TeleportPlayer(destination.position, shwoSnow);

            foreach (GameObject G in AnabolDisabol) 
                G.SetActive(!G.active);
        }
    }
}
