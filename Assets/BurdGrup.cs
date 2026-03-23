using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class BurdGrup : MonoBehaviour
{
    public int NuberOfBurds;
    public LayerMask Grond;
    public float radius;
    public GameObject burd;
    public List<GameObject> activeBurds;
    public bool canSpone = true;
    float t = 0;
    void Update() {
        Vector3 dis = transform.position - GameManager.Instance.PL.position;

        if (dis.magnitude < 50 && activeBurds.Count < NuberOfBurds && canSpone) {
            float ang = Random.Range(0, 360);
            Vector3 Lpos = new Vector3(Mathf.Cos(ang), 1, Mathf.Sin(ang)) * Random.Range(0, radius);
            Ray ray = new Ray(Lpos + transform.position + Vector3.up * 0.5f, Vector3.down);
            if (Physics.Raycast(ray, out var hit, 10, Grond)) {
                activeBurds.Add(Instantiate(burd, hit.point, Quaternion.LookRotation(hit.normal)));
                activeBurds.Last().transform.rotation *= Quaternion.Euler(0, 0, Random.Range(-45, 45));
                activeBurds.Last().transform.localScale *= Random.Range(0.9f, 1.1f);
                activeBurds.Last().transform.SetParent(transform);
            }
        }
        if (dis.magnitude < radius-1 && canSpone) {
            canSpone = false;
            float t = 0;

            foreach (GameObject B in activeBurds) {
                B.GetComponent<Animator>().SetTrigger("Fly");
            }

            StartCoroutine(Timer.StartFrameRepitTill(() => BurdFly(), () => (t += Time.deltaTime) < 10));
            StartCoroutine(Timer.RunAfterCondishen(() => DestrayBurd(), () => (t += Time.deltaTime) > 10));
            activeBurds.Last().GetComponent<AudioSource>().Play();

        }
           
    }
    void DestrayBurd() {
        StartCoroutine(Timer.RunAfterCondishen(() => canSpone = true, () => (transform.position - GameManager.Instance.PL.position).magnitude > 50));
        foreach (GameObject B in activeBurds) {
            Destroy(B);
        }
        activeBurds.Clear();
    }
    
    void BurdFly() {

        int i = 0;
        t += Time.deltaTime;
        foreach (GameObject B in activeBurds) {
            if (t > Vector3.Distance(B.transform.position, GameManager.player.transform.position)/10f) 
                B.transform.position += (B.transform.up + transform.up*0.5f + Vector3.up * Mathf.Sin(Time.time*10 + (i++)*0.1f)) * 10 * Time.deltaTime;
        }
    }
}
