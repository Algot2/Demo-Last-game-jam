using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InterpilatedHurtBox : MonoBehaviour {

    public float radius = 1;
    public LayerMask layerMask;
    public List<Vector3> locolPos;
    List<Vector3> LastPos;


    public float dam;
    bool hasHit;
    void Start() {
        //radius = transform.localScale.x;
        float h = transform.localScale.y;

        int i = 0;
        for (float D = -radius; D < h && i < 100; D += radius) {
            i++;
            locolPos.Add(new Vector3(0, D, 0));
            LastPos.Add(new Vector3(0, D, 0));
        }
    }
    RaycastHit SphereCastBetweenPoints (Vector3 startPoint, Vector3 endPoint) {
        Vector3 direction = endPoint - startPoint;
        float distance = direction.magnitude;
        direction.Normalize();
        RaycastHit hit;
        if (Physics.SphereCast(startPoint, radius, direction, out hit, distance, layerMask)) {
            hasHit = true;
            return hit;
        }

        hasHit = false;
        return default;
    }

    Vector3 ToWord(Vector3 vec) => transform.localToWorldMatrix.MultiplyPoint3x4(vec);

    void LateUpdate()
    {
        for (int i = 0; i < locolPos.Count; i++)
        {
            LastPos[i] = ToWord(locolPos[i]);
        }
    }
    void Update() {
        RaycastHit hit = new();
        bool atack = false;
        for (int i = 0; i < locolPos.Count; i++) {
            Debug.DrawLine(LastPos[i], ToWord(locolPos[i]), Color.yellow);
            hasHit = false;
            hit = SphereCastBetweenPoints(LastPos[i], ToWord(locolPos[i]));
           if (hasHit)
                atack = true;
        }

        if (atack) 
            hit.collider.gameObject.GetComponent<NewHitBox>().onHit(dam, transform.position - hit.point);
    }

    void OnDrawGizmos() {
        for (int i = 0; i < locolPos.Count; i++) {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(ToWord(locolPos[i]), radius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ToWord(LastPos[i]), radius);
        }

    }
}
