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
        if (Vector3.Distance(transform.position, setY(TargetPos, transform.position)) < 1) 
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
