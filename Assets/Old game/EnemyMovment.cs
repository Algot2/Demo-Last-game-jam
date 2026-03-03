using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovment : MonoBehaviour
{
    public Vector3 target;
    public NavMeshAgent agent;
    public Rigidbody rb;


    void Update() {
        agent.SetDestination(target);
        List<BaseEnemyLogic> en = GameManager.Instance.enemies;
        for (int i = 0; i < en.Count; i++)
        {
            Vector3 Dis = transform.position - en[i].transform.position;
            if (Dis.magnitude < 2.5f && en[i].gameObject.GetInstanceID() != gameObject.GetInstanceID())
            {
                rb.AddForce(Dis.normalized * 100 * Time.deltaTime);
            }
        }
    }
}
