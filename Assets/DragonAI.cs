using UnityEngine;
using UnityEngine.AI;

public class DragonAI : MonoBehaviour
{
    public Transform player;
    public Vector3 TargetPos;
    public NavMeshAgent Agent;
    public float disToPl;
    public float sped;

    float R(float min, float max) => Random.Range(min,max);
    Vector3 pikeNewTarget() {
        Vector3 dir = new Vector3(R(-1, 1), R(0.5f, 1), R(-1, 1)).normalized;

      
        return player.position + dir * R(2.5f, disToPl);
    }
    Vector3 setY(Vector3 A, Vector3 B) {
        return new Vector3(A.x, B.y, A.z);
    }
    private void Start()
    {
        TargetPos = pikeNewTarget();
    }
    void Update() {
        if (Vector3.Distance(transform.position, setY(TargetPos, transform.position)) < 1) 
            TargetPos = pikeNewTarget();

        Agent.SetDestination(TargetPos);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(player.position, disToPl);

        Gizmos.DrawWireSphere(TargetPos, 0.5f);
    }
}
