using UnityEngine;

public class FogDesetyMan : MonoBehaviour
{
    public Material fog;
    public float f;
    void Update() {
        fog.SetFloat("_F", f);
    }
}
