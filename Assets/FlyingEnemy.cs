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
    public int rOrl = 1;
    void spone(GameObject obj, bool enemy) {
        GameObject a = Instantiate(obj, transform.position - transform.forward - transform.right, transform.rotation);
        GameObject b = Instantiate(obj, transform.position - transform.forward + transform.right, transform.rotation);
        if (enemy) GameManager.Instance.enemies.Add(a.GetComponentInChildren<BaseEnemyLogic>());
        if (enemy) GameManager.Instance.enemies.Add(b.GetComponentInChildren<BaseEnemyLogic>());
        a.transform.LookAt(GameManager.player);
        b.transform.LookAt(GameManager.player);

        StartCoroutine(Timer.StartTimer(Random.Range(sponDilay*0.5f, sponDilay*1.5f), (f) => canSpone = !f));
        rOrl = Random.Range(0, 2) == 1 ? 1:-1;
    }

    private void Start()
    {
        rOrl = Random.Range(0, 2) == 1 ? 1:-1;
    }


    void Update() {


        Vector3 Pl = GameManager.player.position;
        Vector3 dis = transform.position - Pl;
        moment.target = Pl;

        if (dis.magnitude < aldileDistToPlayer + 0.5f) moment.agent.speed = 0;
        else moment.agent.speed = 3;


        if (dis.magnitude < aldileDistToPlayer + 0.5f) transform.position += dis.normalized * Time.deltaTime * 2;
        else if (dis.magnitude > aldileDistToPlayer - 0.5f) transform.position += transform.right * rOrl * Time.deltaTime * 2;

        if (canSpone && dis.magnitude - aldileDistToPlayer < 1) {
            GameObject obj = Random.Range(0, 5) == 0 ? Bat : Prodektile;
            spone(obj, obj == Bat);
        } 

        Vishols.localPosition = Vector3.up * (0.5f + Mathf.Sin(Time.time * 2)*0.5f);

    }
}
