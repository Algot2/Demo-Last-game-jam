using System.Collections.Generic;
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
    public int RorL;
    float higet = -0.5f;
    private void Start()
    {
        RorL = Random.Range(0,2)==0?-1:1;
         StartCoroutine(Timer.StartTimer(Random.Range(1f, 4f), (f) => canAtack = !f));
    }
    void Update() {
        List<BaseEnemyLogic> en = GameManager.Instance.enemies;
        for (int i = 0; i < en.Count; i++)
        {
            Vector3 dis = transform.position - en[i].transform.position;
            if (dis.magnitude < 1.5f && en[i].gameObject.GetInstanceID() != gameObject.GetInstanceID())
            {
                transform.position += (dis.normalized * Time.deltaTime);
            }
        }

        if (!ded) {

            if (transform.position.y - GameManager.player.position.y < -higet)
                transform.position += Vector3.up * 10 * Time.deltaTime;

            higet = canAtack ? 0.5f : -0.2f;
            transform.LookAt(GameManager.player.position);
            if (HellfSlider.curnt < 0) { 
                ded = true; 
                rb.isKinematic = false;
            }

            Vector3 dir = transform.position - GameManager.player.position;
            transform.position += (dir.normalized * sped * Time.deltaTime) * ((dir.magnitude > 3 || canAtack) ? -1 : 1);


            if (dir.magnitude > 2 && dir.magnitude < 4 && !canAtack) {
                transform.position += transform.right*Time.deltaTime * RorL;
            }
            Body.localPosition = Vector3.up * Mathf.Sin(Time.time * 10) * 0.1f;

            hitbox.SetActive(canAtack);
            if (canAtack && dir.magnitude < 0.4f)
                StartCoroutine(Timer.StartTimer(Random.Range(1f, 4f), (f) => canAtack = !f));
        }
    }
}
