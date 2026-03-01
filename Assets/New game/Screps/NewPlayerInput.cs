using UnityEngine;

public class NewPlayerInput : MonoBehaviour
{
    public CamraMoment cam;
    public PlMoment plMoment;
    public Animator animator;
    public PlAtackMan plAtacks;
    public float sensetivety;
    public bool[] canDo;

    public enum state {
        idel,
        move,
        Jump,
        atack
    }

    public state State;

   
    void Update() {
        Cursor.lockState = CursorLockMode.Locked;
        cam.setCamraDireksen(new Vector2(-Input.mousePositionDelta.x / Screen.width, Input.mousePositionDelta.y / Screen.height) * sensetivety);

        if (State == state.idel) 
            if (canDo[0] && Input.GetMouseButtonDown(0)) {
                canDo[0] = false;
                State = state.atack;
                StartCoroutine(Timer.RunAfterTimer(0.5f, () => State = state.idel));
                StartCoroutine(Timer.RunAfterTimer(1, () => canDo[0] = true));
                plAtacks.PreformAtack(0, 2); 
            }

        if (State == state.idel)
            if (canDo[2] && Input.GetKeyDown(KeyCode.Space)) {
                canDo[2] = false;
                animator.SetTrigger("Jump");
                StartCoroutine(Timer.RunAfterTimer(0.5f, () => canDo[2] = true));

            }

        if (State == state.idel)
            if (canDo[1] && Input.GetKeyDown(KeyCode.LeftShift)) {
                canDo[1] = false;
                plMoment.Dodsh(-new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")));
                StartCoroutine(Timer.RunAfterTimer(1, () => canDo[1] = true));
                animator.SetTrigger("Jump");
            }


        if (State == state.move || State == state.idel) {
            plMoment.Move(new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")));
        }

    }
}
