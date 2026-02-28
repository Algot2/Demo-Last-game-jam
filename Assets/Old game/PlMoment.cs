using UnityEngine;

public class PlMoment : MonoBehaviour
{
    public Rigidbody rb;
    public float Sped;
    public Transform head;
    public Transform body;
    public Animator animator;
    Vector3 Dir = new();
    public void Move(Vector3 input) {
        Vector3 dir = -head.forward;
        Quaternion rotaashen = Quaternion.FromToRotation(Vector3.forward, dir);
        transform.position += (rotaashen * input).normalized * Sped * Time.deltaTime;
        animator.SetBool("Run", input.magnitude != 0);
        Dir = (rotaashen * input).normalized;
    }
    private void LateUpdate()
    {
        animator.SetBool("Run", false);
    }
    void Update()
    {
        body.forward = Vector3.Lerp(body.forward, Dir, Time.deltaTime * 15);
    }

}
