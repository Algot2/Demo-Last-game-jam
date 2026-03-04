using UnityEngine;

public class Fotstep : MonoBehaviour
{
    bool can = true;
    void OnTriggerEnter(Collider other) {
        if (can) {
            Transform T = transform.GetChild(0);
            Destroy(Instantiate(T.gameObject, T.position, T.rotation), 5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(Timer.StartTimer(0.2f, (f) => can = !f));
    }
}
