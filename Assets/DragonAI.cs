using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DragonAI : MonoBehaviour
{
    public static DragonAI Instens;
    public Vector3 TargetPos;
    public NavMeshAgent Agent;
    public GameObject Atacks;
    public Animator Animator;
    public float MinDisToEn;
    public float disToPl;
    public float atacDis;
    public float TimeInbetinAtacks;

    float timer = 0;
    Transform player;
    bool canAtack = true;
    bool targetEn = false;
    public bool tryHellPlayer = false;
    float R(float min, float max) => Random.Range(min,max);
    
    public Vector3 pikeNewTarget() {
        Vector3 dir = new Vector3(R(-1, 1), R(0.5f, 1), R(-1, 1)).normalized;
        Vector3 pos = player.position + dir * R(2.5f, disToPl);

        return pos;
    }
    public Vector3 SnapToNavMesh(Vector3 vec) {
        NavMeshHit hit;

        // Try to find nearest point on NavMesh
        if (NavMesh.SamplePosition(vec, out hit, disToPl*2, NavMesh.AllAreas)) {
            Vector3 newPosition = vec;
            newPosition.y = hit.position.y; // Set height to NavMesh height
            return newPosition;
        }
        else {
            Debug.LogWarning("No NavMesh found near this object.");
        }
        
        return vec;
    }

    Vector3 setY(Vector3 A, Vector3 B) {
        return new Vector3(A.x, B.y, A.z);
    }
    private void Start() {
        player = GameManager.player;
        Instens = this;
        TargetPos = SnapToNavMesh(pikeNewTarget());
    }


    void Update() {

        if (tryHellPlayer) {
            TargetPos = player.position;
            if (Vector3.Distance(transform.position, setY(TargetPos, transform.position)) < 0.5f) {
                tryHellPlayer = false;
                player.GetComponentInChildren<HellfSlider>().setValu(player.GetComponentInChildren<HellfSlider>().curnt + 25);
            }
        }

        Animator.SetBool("Run", Agent.remainingDistance > 0.01f);
        targetEn = false;
        float dis = MinDisToEn;
        for (int i = 0; i < GameManager.Instance.enemies.Count; i++) { 
            Rigidbody R = GameManager.Instance.enemies[i].rb;
            // EnemyControler C = EnMan.EnemysCon[i];
            if (Vector3.Distance(R.position, transform.position) < dis) {
                targetEn = true;
                dis = Vector3.Distance(R.position, transform.position);
                TargetPos = SnapToNavMesh(R.position);
            }
        }

        if (targetEn && canAtack && Vector3.Distance(transform.position, TargetPos) < atacDis) {
            canAtack = false;
            StartCoroutine(Timer.RunAfterTimer(TimeInbetinAtacks, () => canAtack = true));
            StartCoroutine(Timer.StartTimer(0.5f, (f) => Atacks.SetActive(f)));
            Animator.SetTrigger("Atack");
        }


        if ((Vector3.Distance(transform.position, setY(TargetPos, transform.position)) < 0.5f && !canAtack && (timer += Time.deltaTime) > Random.Range(5f, 10f)) ||
           Vector3.Distance(transform.position, player.position) > disToPl) {
            tryHellPlayer = false;
            timer = 0;
            TargetPos = SnapToNavMesh(pikeNewTarget());
        }


        Agent.SetDestination(TargetPos);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if(player != null)
            Gizmos.DrawWireSphere(player.position, disToPl);

        Gizmos.DrawWireSphere(TargetPos, 0.5f);
    }
}
