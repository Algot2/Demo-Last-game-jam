using UnityEngine;

public class NewHuerBox : MonoBehaviour
{
    public bool IsEnemy;
    public float dam;
    void OnTriggerEnter(Collider other) {
        
        NewHitBox hitBox = other.transform.root.GetComponentInChildren<NewHitBox>();
        if (hitBox == null || hitBox.IsEnemy == IsEnemy) return;
        hitBox.onHit(dam);

        if (!IsEnemy) { 
            StartCoroutine(Efects.camShake(0.05f, 0.1f)); 
            StartCoroutine(Efects.timeFrez(0.1f));
        }
    }
}
