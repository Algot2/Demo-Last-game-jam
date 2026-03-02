using UnityEngine;

public class prodectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 0.1f);   
    }

    private void Start() {
        Destroy(gameObject, 5);

    }
    void Update() {

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one*0.75f, Time.deltaTime*25);
        transform.position += transform.forward * 10*Time.deltaTime;
    }
}
