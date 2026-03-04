using UnityEngine;

public class PlMoment : MonoBehaviour
{
    public CharacterController controller;
    public HellfSlider HellfSlider;
    public float Sped;
    public float dodshSped;
    public float dodshMove;

    public bool isDodsh;
    
    public Transform head;
    public Transform body;
    public Animator animator;
    Vector3 Dir = new();
    Vector3 moveDir;
    Vector3 lastMoveDir;
    
    public void Move(Vector3 input) 
    {

        Vector3 dir = -head.forward;
        Quaternion rotaashen = Quaternion.FromToRotation(Vector3.forward, dir);

        if (!isDodsh)
        {
            moveDir = (rotaashen * input).normalized * Sped * Time.deltaTime;
            moveDir.y = -2;

            Vector3 setLastMoveDir = moveDir;
            setLastMoveDir.y = 0;
            setLastMoveDir = setLastMoveDir.normalized;
            if (setLastMoveDir.magnitude > 0)
                lastMoveDir = setLastMoveDir;
        }
        
        animator.SetBool("Run", input.magnitude != 0);
        Dir = (rotaashen * input).normalized;
    }

    public void Dodsh()
    {
        isDodsh = true;
        
        Vector3 dir = -head.forward;
        Quaternion rotaashen = Quaternion.FromToRotation(Vector3.forward, dir);

        Vector3 orgPos = transform.position;
        StartCoroutine(Timer.RunAfterTimer(0.5f, () => isDodsh = false));
        StartCoroutine(Timer.StartTimer(0.5f, (f) => HellfSlider.Inmune = f));
        StartCoroutine(Timer.StartFrameRepitTill(() => 
        { 
            moveDir = lastMoveDir * dodshSped * Time.deltaTime;
        }, () => Vector3.Distance(orgPos, transform.position) < dodshMove));
    }
    private void LateUpdate()
    {
        animator.SetBool("Run", false);
    }
    void Update()
    {
        body.forward = Vector3.Lerp(body.forward, Dir, Time.deltaTime * 15);

        controller.Move(moveDir);
    }

}
