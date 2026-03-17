using UnityEngine;

public class Fotstep : MonoBehaviour
{
    bool can = true;
    public bool SFX = false;
    public AudioClip[] audioClips;
    public AudioSource AudioSource;

    public float scale;

    void OnTriggerEnter(Collider other) {
        LayerMask ground = GameManager.Instance.ground;
        if (can) {
            if (SFX)
            {
                AudioSource.volume = Random.Range(0.2f, 0.25f);
                AudioSource.pitch = Random.Range(0.7f, 0.9f);
                AudioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
            }
            Transform T = transform.GetChild(0);
            GameObject fotStep = Instantiate(T.gameObject, T.position, T.rotation);

            if (Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out var hit, 100, ground))
            {
                fotStep.transform.position = hit.point + Vector3.up * 0.005f;
                float ang = fotStep.transform.rotation.ToEuler().y;
                fotStep.transform.up = hit.normal;
                fotStep.transform.RotateAround(fotStep.transform.TransformDirection(Vector3.up), ang);
                fotStep.SetActive(true);
            }

            Destroy(fotStep, 5);
        }
    }

    private void OnTriggerExit(Collider other)
    {
      StartCoroutine(Timer.StartTimer(0.2f, (f) => can = !f));
    }
}
