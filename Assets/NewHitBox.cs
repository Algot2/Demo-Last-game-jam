using UnityEngine;

public class NewHitBox : MonoBehaviour
{
    public bool IsPlayer;
    public bool IsEnemy;
    public GameObject hitEfect;
    public HellfSlider Slider;
    public Transform BodyPart;
    public ParticleSystem ParticleSystem;
    public float efeckt = 1;
    Vector3 PosOfset = new();
    Quaternion rotashenOfset = new();


    public void onHit(float dam, Vector3 back) {
        Slider.setValu(Slider.curnt - dam*efeckt);
        if (IsEnemy||Slider.Inmune) ParticleSystem.Play();

        if (IsPlayer && !Slider.Inmune) StartCoroutine(Timer.StartTimer(0.1f, (f) => hitEfect.SetActive(f)));
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
