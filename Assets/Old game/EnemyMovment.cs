using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovment : MonoBehaviour
{
    public Vector3 target;
    public NavMeshAgent agent;



    void Update() {
        agent.SetDestination(target);
    }
}
