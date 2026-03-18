using UnityEngine;

public class NewHitBox : MonoBehaviour
{
    public bool IsPlayer;
    public bool IsEnemy;
    public GameObject hitEfect;
    public HellfSlider Slider;
    public Transform BodyPart;
    public ParticleSystem ParticleSystem;
    public AudioSource AudioSource;
    public AudioClip Block, Hit;
    public float efeckt = 1;
    Vector3 PosOfset = new();
    Quaternion rotashenOfset = new();


    public void onHit(float dam, Vector3 back) {
        Debug.Log("hit");
        Slider.setValu(Slider.curnt - dam*efeckt);
        if (IsEnemy||Slider.ImuneSwitsh) ParticleSystem.Play();
        
        if (Slider.ImuneSwitsh) { 
            AudioSource.PlayOneShot(Block);
            AudioSource.volume = Random.Range(0.1f, 0.15f);
            AudioSource.pitch = Random.Range(0.7f, 0.9f);
        }

        if (IsPlayer && !Slider.ImuneSwitsh) {
            StartCoroutine(Timer.StartTimer(0.2f, (f) => hitEfect.SetActive(f)));
            AudioSource.PlayOneShot(Hit);
            AudioSource.volume = Random.Range(0.1f, 0.15f);
            AudioSource.pitch = Random.Range(0.8f, 1.2f);
        }
    }

    void OnEnable()
    {
        if (BodyPart != null)
        {
            PosOfset = transform.position - BodyPart.position;
            rotashenOfset = transform.rotation * Quaternion.Inverse(BodyPart.rotation);
        }
    }

    void Update() {
        ////transform.position = BodyPart.position - BodyPart.TransformDirection(PosOfset);
        ////transform.rotation = BodyPart.rotation * Quaternion.Inverse(rotashenOfset);
    }
}
