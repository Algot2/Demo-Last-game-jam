using Unity.VisualScripting;
using UnityEngine;

public class FlikerLite : MonoBehaviour
{
    public Light theLight;
    public AnimationCurve curve;
    public float sped;
    public float britnes;
    float molt = 1;
    float curentTime = 0;

    private void Start() {
        if (theLight == null)
        {
            theLight = GetComponent<Light>() != null ? 
                GetComponent<Light>() : 
                GetComponentInChildren<Light>();
        }
        britnes = theLight.intensity;
    }
    void Update() {
        curentTime += Time.deltaTime*sped;

        if (curentTime >= 1) { 
            curentTime--;
            molt = Random.Range(0.6f, 1.4f);
        }
        theLight.intensity = curve.Evaluate(curentTime) * molt * britnes;

    }
}
