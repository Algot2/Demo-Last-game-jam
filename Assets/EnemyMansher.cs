using System.Collections.Generic;
using UnityEngine;

public class EnemyMansher : MonoBehaviour
{
    public List<Rigidbody> Enemys;
    public float MinDis;
    void Update() {
        for (int i = 0; i < Enemys.Count; i++) 
            for (int j = 0; j < Enemys.Count; j++) {
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
