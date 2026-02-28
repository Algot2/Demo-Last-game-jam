using UnityEngine;

public class EnemySponer : MonoBehaviour
{
    public GameObject[] gameObjects;

    private void OnTriggerEnter(Collider other) {
        foreach (var obj in gameObjects) { 
            obj.SetActive(true);
        }
    }
}
