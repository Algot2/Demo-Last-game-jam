using UnityEngine;

public class prodectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 0.1f);   
    }
    void Update() {
        transform.position += transform.forward * 10*Time.deltaTime;
    }
}
