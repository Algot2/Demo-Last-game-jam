using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public float dam, Nokback, timer;

    private void OnTriggerEnter(Collider other) {
        other.GetComponentInParent<HitBox>().onHit(dam, (transform.position - other.gameObject.transform.position).normalized * Nokback);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x/2f);
    }
}
