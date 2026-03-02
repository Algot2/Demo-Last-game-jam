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
        GameManager.Instance.enemies.Add(Instantiate(Bat, transform.position + transform.forward - transform.right, transform.rotation).GetComponentInChildren<BaseEnemyLogic>());
        GameManager.Instance.enemies.Add(Instantiate(Bat, transform.position + transform.forward + transform.right, transform.rotation).GetComponentInChildren<BaseEnemyLogic>());
        StartCoroutine(Timer.StartTimer(Random.Range(sponDilay*0.5f, sponDilay*1.5f), (f) => canSpone = !f));
    }
    void Update() {

        Vector3 Pl = GameManager.player.position;
        Vector3 dis = transform.position - Pl;
        moment.target = Pl;

        if (dis.magnitude < aldileDistToPlayer + 0.5f) moment.agent.speed = 0;
        else moment.agent.speed = 3;


        if (dis.magnitude < aldileDistToPlayer + 0.5f) transform.position += dis.normalized * Time.deltaTime;
        else if (dis.magnitude > aldileDistToPlayer - 0.5f) transform.position -= transform.right * Time.deltaTime;

        if (canSpone && dis.magnitude - aldileDistToPlayer < 1)
            sponeBats();

        Vishols.localPosition = Vector3.up * (0.5f + Mathf.Sin(Time.time * 2)*0.5f);

    }
}
