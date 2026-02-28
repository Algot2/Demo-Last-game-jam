using UnityEngine;

public class NewHitBox : MonoBehaviour
{
    public bool IsEnemy;
    public HellfSlider Slider;
    public Transform BodyPart;
    public ParticleSystem ParticleSystem;
    Vector3 PosOfset = new();
    Quaternion rotashenOfset = new();


    public void onHit(float dam) {
        if (IsEnemy) ParticleSystem.Play();
        else GetComponent<EnemyControler>().Atackdureashen = 1;
        Slider.setValu(Slider.curnt - dam);
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
