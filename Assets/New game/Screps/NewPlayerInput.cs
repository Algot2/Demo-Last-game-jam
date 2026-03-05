using UnityEngine;

public class NewPlayerInput : MonoBehaviour
{
    public static NewPlayerInput Instance;
    public CamraMoment cam;
    public PlMoment plMoment;
    public Animator animator;
    public PlAtackMan plAtacks;
    public HellfSlider hellfSlider;
    public float sensetivety;
    public bool[] canDo;

    public bool isPaused;

    public static float globalSensitivity = 50;

    public enum state {
        idel,
        move,
        Jump,
        atack
    }

    public state State;

    private void Start() 
    {
        Instance = this;
        
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update() 
    {
        cam.setCamraDireksen(-new Vector2(Input.mousePositionDelta.x / Screen.width, Input.mousePositionDelta.y / Screen.height) * sensetivety, Input.mouseScrollDelta.y);

        if (State == state.idel) 
            if (canDo[0] && Input.GetMouseButtonDown(0)) {
                canDo[0] = false;
                State = state.atack;
                StartCoroutine(Timer.RunAfterTimer(0.5f, () => State = state.idel));
                StartCoroutine(Timer.RunAfterTimer(1.5f, () => canDo[0] = true));
                plAtacks.PreformAtack(0, 2); 
            }

        if (State == state.idel)
            if (canDo[3] && Input.GetMouseButton(1)) {
                hellfSlider.Inmune = true;
                plMoment.Sped = 1.5f;
            }
            else { 
                if (canDo[3] && Input.GetMouseButtonUp(1))
                    hellfSlider.Inmune = false;
                plMoment.Sped = 3f; 
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
                plMoment.Dodsh();
                StartCoroutine(Timer.RunAfterTimer(1, () => canDo[1] = true));
                animator.SetTrigger("Jump");
            }



        if (State == state.move || State == state.idel) {
            if (new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")).magnitude > 0) {
                plMoment.Move(new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")));
            }
        }


        if (State == state.idel)
            if (canDo[4] && Input.GetKeyDown(KeyCode.LeftControl))
            {
                canDo[4] = false;
                DragonAI.Instens.TargetPos = transform.position;

                StartCoroutine(Timer.RunAfterTimer(10, () => canDo[4] = true));
                StartCoroutine(Timer.RunAfterCondishen(() => hellfSlider.setValu(hellfSlider.curnt + 35),() => Vector3.Distance(DragonAI.Instens.transform.position, transform.position) < 1));
                animator.SetTrigger("Jump");
            }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            
            if(isPaused)
                PlayerUIController.Instance.Pause();
            else
                PlayerUIController.Instance.ResumeGame();
        }
        
        if (globalSensitivity != sensetivety)
        {
            sensetivety = globalSensitivity;
        }
    }
}
