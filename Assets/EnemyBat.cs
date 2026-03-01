using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    public float sped;
    public Transform Body;
    public bool canAtack;
    public GameObject hitbox;
    public HellfSlider HellfSlider;
    public Rigidbody rb;
    public bool ded;
    void Update() {
        if (!ded) {
            if (HellfSlider.curnt < 0) { 
                ded = true; 
                rb.isKinematic = false;
                StartCoroutine(Timer.RunAfterTimer(1, () => Destroy(gameObject)));
            }

            Vector3 dir = transform.position - EnemyMansher.player.position;
            transform.position += (dir.normalized * sped * Time.deltaTime) * ((dir.magnitude > 2 || canAtack) ? -1 : 1);
            Body.localPosition = Vector3.up * Mathf.Sin(Time.time * 10) * 0.1f;

            hitbox.SetActive(canAtack);
            if (canAtack && dir.magnitude < 0.1f)
                StartCoroutine(Timer.StartTimer(Random.Range(0.5f, 2f), (f) => canAtack = !f));
        }
    }
}
