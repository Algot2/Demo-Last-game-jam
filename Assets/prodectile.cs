using UnityEngine;

public class prodectile : MonoBehaviour
{
    public float Scale = 1;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);   
    }

    private void Start() {
        Destroy(gameObject, 5);

    }
    void Update() {

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one* Scale, Time.deltaTime*15);
        transform.position += transform.forward * 10*Time.deltaTime;
    }
}
