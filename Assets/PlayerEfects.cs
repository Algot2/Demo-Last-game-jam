using UnityEngine;
using UnityEngine.UI;

public class PlayerEfects : MonoBehaviour
{
    public HellfSlider Imunetys;
    public Slider slider;
    void Update() {
        slider.value = Imunetys.imune;
    }
}
