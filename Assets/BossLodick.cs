using UnityEngine;

public class BossLodick : MonoBehaviour
{
    public Transform LiteA, LiteB;
    public GameObject Enemy1, Prodektile;
    public GameObject Enemy2;
    public EnemyMovment moment;
    public Transform Vishols;
    public float sponDilay;
    public float aldileDistToPlayer;
    public HellfSlider hellf;
    public Animator sord;
    public Fase curent;
    int rOrl = 1;
    float higet = 1.5f;
    public bool canSpone = true;


    bool canMaleyAtack = true;
    public enum Fase {
        Fase1,
        Fase2,
        Fase3
    }
    void spone(GameObject obj, bool enemy)
    {
        GameObject a = Instantiate(obj, transform.position - transform.forward - transform.right * 5 + Vector3.up * (!enemy?10:0), transform.rotation);
        GameObject b = Instantiate(obj, transform.position - transform.forward + transform.right * 5 + Vector3.up * (!enemy ? 10 : 0), transform.rotation);
        if (enemy) GameManager.Instance.enemies.Add(a.GetComponentInChildren<BaseEnemyLogic>());
        if (enemy) GameManager.Instance.enemies.Add(b.GetComponentInChildren<BaseEnemyLogic>());
        a.transform.LookAt(GameManager.player);
        b.transform.LookAt(GameManager.player);

        StartCoroutine(Timer.StartTimer(Random.Range(sponDilay * 0.5f, sponDilay * 1.5f), (f) => canSpone = !f));
        rOrl = Random.Range(0, 2) == 1 ? 1 : -1;
    }

    private void Start()  {
        rOrl = Random.Range(0, 2) == 1 ? 1 : -1;
    }


    void Update() {
        if (curent == Fase.Fase1 && hellf.curnt < 1000)
            curent = Fase.Fase2;
        if (curent == Fase.Fase2 && hellf.curnt < 500)
            curent = Fase.Fase3;

        Vector3 Pl = GameManager.player.position;
        Vector3 dis = transform.position - Pl;
        moment.target = Pl;

        //LongReash moment
        if (curent == Fase.Fase1 || !canMaleyAtack) {
            if (dis.magnitude < aldileDistToPlayer + 0.5f) moment.agent.speed = 0;
            else moment.agent.speed = 3;

            if (dis.magnitude < aldileDistToPlayer + 0.5f) transform.position += dis.normalized * Time.deltaTime * 2;
            else if (dis.magnitude > aldileDistToPlayer - 0.5f) transform.position += transform.right * rOrl * Time.deltaTime * 2;

            if (canSpone && dis.magnitude - aldileDistToPlayer < 5) {
                GameObject obj = (Random.Range(0, curent==Fase.Fase3?2:5) == 0 && curent != Fase.Fase1) ? Enemy1 : Prodektile;
                spone(obj, obj != Prodektile);

                if (curent == Fase.Fase3 && Random.Range(0, 10) == 0)
                    spone(Enemy2, true);

            }
        }

        //MalayMoment
        if (curent != Fase.Fase1 && canMaleyAtack) {
            moment.agent.speed = 5;

            if (dis.magnitude < 1.5f) {
                sord.SetTrigger("A");
                StartCoroutine(Timer.StartTimer(20, (f) => canMaleyAtack = !f));
            }
        }

        Vishols.localPosition = Vector3.up * (higet + 0.5f + Mathf.Sin(Time.time * 2) * 0.5f);
        LiteA.LookAt(GameManager.player);
        LiteB.LookAt(GameManager.player);
    }

    
}
