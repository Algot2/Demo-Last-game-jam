using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public GameObject Bat;
    public EnemyMovment moment;
    public Transform Vishols;
    public float sponDilay;
    public float aldileDistToPlayer;
    public bool canSpone;
    void sponeBats() {
        Instantiate(Bat, transform.position + transform.forward, transform.rotation);
        Instantiate(Bat, transform.position + transform.forward, transform.rotation);
        StartCoroutine(Timer.StartTimer(Random.Range(sponDilay*0.5f, sponDilay*1.5f), (f) => canSpone = !f));
    }
    void Update() {
        Vector3 Pl = EnemyMansher.player.position;
        Vector3 dis = transform.position - Pl;

        if (dis.magnitude > aldileDistToPlayer - 1) moment.target = Pl;
        else if (dis.magnitude < aldileDistToPlayer + 1) moment.target = transform.position + dis.normalized;
        else moment.target = transform.position += transform.right * Random.Range(-1, 2);

        if (canSpone && dis.magnitude - aldileDistToPlayer < 1)
            sponeBats();

    }
}
