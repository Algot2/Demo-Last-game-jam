using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public GameObject Bat;
    public GameObject Prodektile;
    public EnemyMovment moment;
    public Transform Vishols;
    public float sponDilay;
    public float aldileDistToPlayer;
    public bool canSpone;
    void spone(GameObject obj) {
        GameObject a = Instantiate(obj, transform.position - transform.forward - transform.right, transform.rotation);
        GameObject b = Instantiate(obj, transform.position - transform.forward + transform.right, transform.rotation);
        GameManager.Instance.enemies.Add(a.GetComponentInChildren<BaseEnemyLogic>());
        GameManager.Instance.enemies.Add(b.GetComponentInChildren<BaseEnemyLogic>());
        a.transform.LookAt(GameManager.player);
        b.transform.LookAt(GameManager.player);

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
            spone(Random.Range(0, 5) == 0 ? Bat: Prodektile);

        Vishols.localPosition = Vector3.up * (0.5f + Mathf.Sin(Time.time * 2)*0.5f);

    }
}
