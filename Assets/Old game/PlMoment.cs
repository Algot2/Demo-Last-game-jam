using System;
using UnityEngine;

public class PlMoment : MonoBehaviour
{
    public CharacterController controller;
    public HellfSlider HellfSlider;
    public float Sped;
    public float parrySpeed = 1.5f;

    public float currentSpeed;
    
    public float dodshSped;
    public float dodshMove;

    public float gravity = 10;

    public bool isDodsh;

    public bool canMove = true;
    
    public Transform head;
    public Transform body;
    public Animator animator;
    Vector3 Dir = new();
    Vector3 moveDir;
    Vector3 lastMoveDir;
    Vector3 curentPos;
    
    public void Move(Vector3 input) 
    {
        Vector3 dir = -head.forward;
        Quaternion rotaashen = Quaternion.FromToRotation(Vector3.forward, dir);

        if (!isDodsh && canMove)
        {
            moveDir = (rotaashen * input).normalized * currentSpeed * Time.deltaTime;
            
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

        Vector3 orgPos = transform.position;
        orgPos.y = 0;
        StartCoroutine(Timer.StartTimer(0.5f, (f) => HellfSlider.Inmune = f));
        StartCoroutine(Timer.StartFrameRepitTill(() =>
            {
                moveDir = lastMoveDir * dodshSped * Time.deltaTime;
            }, () => Vector3.Distance(orgPos, curentPos) < dodshMove,
            () =>
            {
                isDodsh = false;
                Move(new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")));
            }));
    }
    private void LateUpdate()
    {
        animator.SetBool("Run", false);
    }
    void Update()
    {
        body.forward = Vector3.Lerp(body.forward, Dir, Time.deltaTime * 15);

        curentPos = transform.position;
        curentPos.y = 0;
        
        if (!canMove && !isDodsh)
            moveDir = Vector3.zero;
        
        moveDir.y = -gravity * Time.deltaTime;
        controller.Move(moveDir);
    }

    private void Awake()
    {
        currentSpeed = Sped;
    }
}
