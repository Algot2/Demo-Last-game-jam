using UnityEngine;
using UnityEngine.SceneManagement;

public class HitBox : MonoBehaviour
{
    public Rigidbody rb;
    public HellfSlider HellfSlider;
    public ParticleSystem hitefect;
    public ParticleSystem Blokfect;
    public float hellf = 100;
    public bool hitStop, resetOnDef, Bloking;
    public void onHit(float dam, Vector3 nokback) {
        if (!Bloking)
        {
            HellfSlider.gameObject.SetActive(true);

            rb.AddForce(nokback);
            hellf -= dam;
            if (hitStop) hitefect.Play();
            if (hitStop && dam > 5)
            {
                StartCoroutine(Efects.timeFrez((dam - 5) / 25f));
                StartCoroutine(Efects.camShake((dam - 5) / 100f, (dam - 5) / 50f));
            }

            if (hellf <= 0)
            {
                HellfSlider.gameObject.SetActive(false);
                if (resetOnDef)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            HellfSlider.setValu(hellf);
        }
        else
        {
            Blokfect.Play();
        }
    }

    private void OnDestroy() {
        StopAllCoroutines();
        Time.timeScale = 1;
    }
}
