using UnityEngine;

public class PlayerEfects : MonoBehaviour
{
    public Material player;
    public HellfSlider Imunetys;
    public Color GlowColor;
    void Update() {
        player.SetColor("_EmissionColor", GlowColor * (Imunetys.imune+2));
    }
}
