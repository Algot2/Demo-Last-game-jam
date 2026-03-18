using UnityEngine;

public class FadeOute : MonoBehaviour
{
    public Material material;
    public float a;
    void Update() {
        material.SetFloat("_Transparensy", 1 - Vector3.Distance(transform.position, Camera.main.transform.position)/a);
    }
}
