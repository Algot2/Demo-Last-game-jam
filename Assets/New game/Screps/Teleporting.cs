using UnityEngine;

public class Teleporting : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerg;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.player.position = destination.position;
            //playerg.SetActive(false);
            //playerg.SetActive(true);
        }
    }
}
