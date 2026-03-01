using UnityEngine;

public class DragonAI : MonoBehaviour
{
    public Transform player;
    public Vector3 TargetPos;
    public Vector3 OldTargetPos;
    public float disToPl;
    public float sped;
    float R(float min, float max) => Random.Range(min,max);
    Vector3 pikeNewTarget() {
        Vector3 dir = new Vector3(R(-1, 1), R(0.5f, 1), R(-1, 1)).normalized;

        return player.position + dir * R(2.5f, disToPl);
    }

    void Update() {
        if (Vector3.Distance(transform.position, TargetPos) < 1) {
            OldTargetPos = TargetPos;
            TargetPos = pikeNewTarget();
        }

        transform.position = Vector3.Lerp(OldTargetPos, TargetPos, Time.deltaTime * sped);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(player.position, disToPl);
    }
}
