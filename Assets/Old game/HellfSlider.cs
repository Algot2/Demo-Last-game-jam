using UnityEngine;
using UnityEngine.UI;

public class HellfSlider : MonoBehaviour
{
    public float max, curnt;
    public Slider valu, frontjump;

    private void Start()
    {
        valu.maxValue = max;
        frontjump.maxValue = max;

        valu.value = curnt;
        frontjump.value = curnt;
    }
    public void setValu(float val)
    {
        curnt = val;
        frontjump.value = val;
    }

    void Update() {
        transform.LookAt(Camera.main.transform);
        transform.forward = -transform.forward;
        valu.value = Mathf.Lerp(valu.value, curnt, Time.deltaTime * 10);
    }
}
