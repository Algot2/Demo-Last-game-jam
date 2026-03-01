using UnityEngine;

public class NewPlayerInput : MonoBehaviour
{
    public CamraMoment cam;
    public PlMoment plMoment;
    public PlAtackMan plAtacks;
    public float sensetivety;
    public bool[] canDo;
    public enum state {
        idel,
        move,
        atack
    }

    public state State;

   
    void Update() {
        Cursor.lockState = CursorLockMode.Locked;
        cam.setCamraDireksen(new Vector2(-Input.mousePositionDelta.x / Screen.width, Input.mousePositionDelta.y / Screen.height) * sensetivety);

        if (State == state.idel) 
            if (Input.GetMouseButtonDown(0) && canDo[0]) {
                canDo[0] = false;
                State = state.atack;
                StartCoroutine(Timer.RunAfterTimer(0.5f, () => State = state.idel));
                StartCoroutine(Timer.RunAfterTimer(1, () => canDo[0] = true));
                plAtacks.PreformAtack(0, 2); 
            }

        if (State == state.move || State == state.idel)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDo[1]) {
                canDo[1] = false;
                plMoment.Dodsh(new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")));
                StartCoroutine(Timer.RunAfterTimer(1, () => canDo[1] = true));
            }

            plMoment.Move(new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")));
        }

    }
}
