using UnityEngine;

public class PlMoment : MonoBehaviour
{
    public Rigidbody rb;
    public HellfSlider HellfSlider;
    public float Sped;
    public float dodshSped;
    public float dodshMove;
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

    public void Dodsh(Vector3 input) {
        Vector3 dir = -head.forward;
        Quaternion rotaashen = Quaternion.FromToRotation(Vector3.forward, dir);

        Vector3 orgPos = transform.position;
        StartCoroutine(Timer.StartTimer(0.5f, (f) => HellfSlider.Inmune = f));
        StartCoroutine(Timer.StartFrameRepitTill(() => 
        { 
            transform.position += (rotaashen * input).normalized * dodshSped * Time.deltaTime;
        }, () => Vector3.Distance(orgPos, transform.position) < dodshMove));
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
