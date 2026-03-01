using UnityEngine;

public class NewHitBox : MonoBehaviour
{
    public bool IsEnemy;
    public HellfSlider Slider;
    public Transform BodyPart;
    public ParticleSystem ParticleSystem;
    public Rigidbody Rb;
    public float efeckt = 1;
    Vector3 PosOfset = new();
    Quaternion rotashenOfset = new();


    public void onHit(float dam, Vector3 back) {
        Rb.AddForce(back);
        Slider.setValu(Slider.curnt - dam*efeckt);
        if (IsEnemy||Slider.Inmune) ParticleSystem.Play();
    }

    void OnEnable()
    {
        PosOfset = transform.position-BodyPart.position;
        rotashenOfset = transform.rotation * Quaternion.Inverse(BodyPart.rotation);
    }

    void Update() {
        transform.position = BodyPart.position - BodyPart.TransformDirection(PosOfset);
        transform.rotation = BodyPart.rotation * Quaternion.Inverse(rotashenOfset);
    }
}
