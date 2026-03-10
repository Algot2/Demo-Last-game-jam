using UnityEngine;

public class FlikerLite : MonoBehaviour
{
    public Light light;
    public AnimationCurve curve;
    public float sped;
    public float britnes;
    float molt = 1;
    float curentTime = 0;

    private void Start() {
        britnes = light.intensity;
    }
    void Update() {
        curentTime += Time.deltaTime*sped;

        if (curentTime >= 1) { 
            curentTime--;
            molt = Random.Range(0.6f, 1.4f);
        }
        light.intensity = curve.Evaluate(curentTime) * molt * britnes;

    }
}
