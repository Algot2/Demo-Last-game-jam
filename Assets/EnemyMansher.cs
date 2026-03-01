using System.Collections.Generic;
using UnityEngine;

public class EnemyMansher : MonoBehaviour
{
    public List<Rigidbody> Enemys;
    public List<EnemyControler> EnemysCon;
    public float MinDis;
    public Transform PL;
    public static Transform player;

    private void Start()
    {
        player = PL;
    }
    void Update() {
        for (int i = 0; i < Enemys.Count; i++) 
            for (int j = 0; j < Enemys.Count; j++) {

                if (EnemysCon[i].ded) {
                    Rigidbody rb = Enemys[i];

                    Enemys.RemoveAt(i);
                    EnemysCon.RemoveAt(i);

                    StartCoroutine(Timer.RunAfterTimer(2, () => Destroy(rb.transform.parent.gameObject)));
                }

                Rigidbody rbA = Enemys[i];
                Rigidbody rbB = Enemys[j];
                Vector3 dis = rbB.position - rbA.position;

                if (dis.magnitude < MinDis)
                {
                    rbA.AddForce(Vector3.one * MinDis - dis);
                    rbB.AddForce(-(Vector3.one * MinDis - dis));
                }
            }
    }
}
