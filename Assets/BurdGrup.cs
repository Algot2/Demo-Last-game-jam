using System.Collections.Generic;
using UnityEngine;

public class BurdGrup : MonoBehaviour
{
    public int NuberOfBurds;
    public LayerMask Grond;
    public float radius;
    public GameObject burd;
    public List<GameObject> activeBurds;
    public bool canSpone = true;
    void Update() {
        Vector3 dis = transform.position - GameManager.Instance.PL.position;

        if (dis.magnitude < 50 && activeBurds.Count < NuberOfBurds && canSpone) {
            float ang = Random.Range(0, 360);
            Vector3 Lpos = new Vector3(Mathf.Cos(ang), 1, Mathf.Sin(ang)) * Random.Range(0, radius);
            Ray ray = new Ray(Lpos + transform.position + Vector3.up * 0.5f, Vector3.down);
            if (Physics.Raycast(ray, out var hit, 10, Grond)) {
                activeBurds.Add(Instantiate(burd, hit.point, Quaternion.LookRotation(hit.normal)));
            }
        }
        if (dis.magnitude < 6 && canSpone) {
            canSpone = false;
            float t = 0;
            StartCoroutine(Timer.StartFrameRepitTill(() => BurdFly(), () => (t += Time.deltaTime) < 5));
            StartCoroutine(Timer.RunAfterCondishen(() => DestrayBurd(), () => (t += Time.deltaTime) > 5));
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

        Debug.Log("a");
        int i = 0;
        foreach (GameObject B in activeBurds) {
              B.transform.position += (transform.forward + transform.up*0.5f + Vector3.up * Mathf.Sin(Time.time*10 + (i++)*0.1f)) * 10 * Time.deltaTime;
        }
    }
}
