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
    public Transform vishols;
    public Transform RotashenTarget;
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
        if (Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out var hit, 100, GameManager.Instance.ground))
        {
            RotashenTarget.localRotation = Quaternion.Euler(Vector3.right);
            float ang = RotashenTarget.transform.rotation.ToEuler().y;
            RotashenTarget.transform.up = hit.normal;
            RotashenTarget.transform.RotateAround(RotashenTarget.transform.TransformDirection(Vector3.up), ang);
        }
        vishols.transform.rotation = Quaternion.Lerp(vishols.transform.rotation, RotashenTarget.transform.rotation, Time.deltaTime*5);
        body.forward = Vector3.Lerp(body.forward, Dir, Time.deltaTime * 15);

        curentPos = transform.position;
        curentPos.y = 0;
        
        if (!canMove && !isDodsh)
            moveDir = Vector3.zero;
        
        animator.SetBool("Run", moveDir.magnitude != 0 && !isDodsh && canMove);
        
        moveDir.y = -gravity * Time.deltaTime;
        controller.Move(moveDir);
    }

    private void Awake()
    {
        currentSpeed = Sped;
    }
}
