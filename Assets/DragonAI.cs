using UnityEngine;
using UnityEngine.AI;

public class DragonAI : MonoBehaviour
{
    public Transform player;
    public Vector3 TargetPos;
    public NavMeshAgent Agent;
    public EnemyMansher EnMan;
    public GameObject Atacks;

    public float MinDisToEn;
    public float disToPl;
    public float atacDis;
    public float TimeInbetinAtacks;
    bool canAtack = true;
    bool targetEn = false;
    float R(float min, float max) => Random.Range(min,max);
    Vector3 pikeNewTarget() {
        Vector3 dir = new Vector3(R(-1, 1), R(0.5f, 1), R(-1, 1)).normalized;
        Vector3 pos = player.position + dir * R(2.5f, disToPl);


        return pos;
    }
    public Vector3 SnapToNavMesh(Vector3 vec)
    {
        NavMeshHit hit;

        // Try to find nearest point on NavMesh
        if (NavMesh.SamplePosition(vec, out hit, disToPl*2, NavMesh.AllAreas))
        {
            Vector3 newPosition = vec;
            newPosition.y = hit.position.y; // Set height to NavMesh height
            return newPosition;
        }
        else
        {
            Debug.LogWarning("No NavMesh found near this object.");
        }

        return vec;
    }

    Vector3 setY(Vector3 A, Vector3 B) {
        return new Vector3(A.x, B.y, A.z);
    }
    private void Start()
    {
        TargetPos = SnapToNavMesh(pikeNewTarget());
    }


    void Update() {
       
        targetEn = false;
        float dis = MinDisToEn;
        for (int i = 0; i < EnMan.Enemys.Count; i++) { 
            Rigidbody R = EnMan.Enemys[i];
            EnemyControler C = EnMan.EnemysCon[i];
            if (Vector3.Distance(R.position, transform.position) < dis && !C.ded) {
                targetEn = true;
                dis = Vector3.Distance(R.position, transform.position);
                TargetPos = SnapToNavMesh(R.position);
            }
        }

        if (targetEn && canAtack && Vector3.Distance(transform.position, TargetPos) < atacDis) {
            canAtack = false;
            StartCoroutine(Timer.RunAfterTimer(TimeInbetinAtacks, () => canAtack = true));
            StartCoroutine(Timer.StartTimer(0.5f, (f) => Atacks.SetActive(f)));
        }


        if (Vector3.Distance(transform.position, setY(TargetPos, transform.position)) < 1 ||
           Vector3.Distance(transform.position, player.position) > disToPl ||
           !canAtack)
            TargetPos = SnapToNavMesh(pikeNewTarget());


        Agent.SetDestination(TargetPos);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(player.position, disToPl);

        Gizmos.DrawWireSphere(TargetPos, 0.5f);
    }
}
