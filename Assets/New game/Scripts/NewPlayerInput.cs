using UnityEngine;
using BoltsTools;
using UnityEngine.UI;
using Unity.VisualScripting;

public class NewPlayerInput : MonoBehaviour
{
    public static NewPlayerInput Instance;
    public CamraMoment cam;
    public PlMoment plMoment;
    public Animator animator;
    public PlAtackMan plAtacks;
    public HellfSlider hellfSlider;
    public Slider Stamina;
    public float sensetivety;
    public bool[] canDo;
    public bool isDed;
    public bool isPaused;
    public Material screenShade;
    [BoltsShaderProperty("screenShade")]
    public string shade;
    public bool runTogel;
    public static float globalSensitivity = 50, globalBrightnes = 2;

    float currentBrightnes;
    
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
    void Update()  {
        if (!isDed)
        {
            if (Input.GetKeyDown(KeyCode.R))
                runTogel = !runTogel;
            
            if (Stamina.value <= 0 || new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")).magnitude == 0 || Input.GetMouseButtonDown(0)) 
                runTogel = false;
            animator.speed = (runTogel ? 1.5f : 1);
            
            if (runTogel) plMoment.currentSpeed = plMoment.Sped * 1.5f;
            else {
                plMoment.currentSpeed = plMoment.Sped;
                if (Stamina.value < Stamina.maxValue) Stamina.value += Time.deltaTime; 
            }

            cam.setCamraDireksen(-new Vector2(Input.mousePositionDelta.x / Screen.width, Input.mousePositionDelta.y / Screen.height) * sensetivety, Input.mouseScrollDelta.y);

            if (State == state.idel)
                if (canDo[0] && Input.GetMouseButtonDown(0))
                {
                    canDo[0] = false;

                    plMoment.canMove = false;

                    State = state.atack;
                    StartCoroutine(Timer.RunAfterTimer(0.5f, () => State = state.idel));
                    StartCoroutine(Timer.RunAfterTimer(1.5f, () =>
                    {
                        canDo[0] = true;
                        plMoment.canMove = true;
                    }));
                    plAtacks.PreformAtack(0, 2);
                }

            if (State == state.idel) { 

                if (canDo[3] && Input.GetMouseButtonDown(1)) {
                    hellfSlider.imune++;
                    plMoment.currentSpeed = plMoment.parrySpeed;
                }

                if ((!canDo[3] && plMoment.currentSpeed == plMoment.parrySpeed) || Input.GetMouseButtonUp(1)) {
                    plMoment.currentSpeed = plMoment.Sped;
                    hellfSlider.imune--;
                }
            }

            if (State == state.idel)
                if (canDo[2] && Input.GetKeyDown(KeyCode.Space))
                {
                    canDo[2] = false;
                    animator.SetTrigger("Jump");
                    StartCoroutine(Timer.RunAfterTimer(0.5f, () => canDo[2] = true));

                }

            if (State == state.idel)
                if (canDo[1] && Input.GetKeyDown(KeyCode.LeftShift))
                {
                    canDo[1] = false;
                    plMoment.Dodsh();
                    StartCoroutine(Timer.RunAfterTimer(1, () => canDo[1] = true));
                    animator.SetTrigger("Jump");
                }


            if (State == state.move || State == state.idel)
            {
                if (new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")).magnitude > 0)
                {
                    plMoment.canMove = true;
                    plMoment.Move(new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")));
                  
                    
                        if (runTogel) Stamina.value -= Time.deltaTime * 0.5f;
                }
                else
                    plMoment.canMove = false;
            }


            if (State == state.idel)
                if (canDo[4] && Input.GetKeyDown(KeyCode.LeftControl))
                {
                    canDo[4] = false;

                    DragonAI.Instens.tryHellPlayer = true;
                    StartCoroutine(Timer.RunAfterTimer(10, () => canDo[4] = true));
                }

            if (Input.GetKeyDown(KeyCode.Escape) && hellfSlider.curnt > 0 && !BoltsCommands.isTyping)
            {
                isPaused = !isPaused;

                if (isPaused)
                    PlayerUIController.Instance.Pause();
                else
                    PlayerUIController.Instance.ResumeGame();
            }

            if (globalSensitivity != sensetivety)
            {
                sensetivety = globalSensitivity;
            }

            if (globalBrightnes != currentBrightnes)
            {
                currentBrightnes = globalBrightnes;
                screenShade.SetFloat(shade, currentBrightnes);
            }
        }
        else { 
            animator.SetBool("Ded", true); 
            plMoment.canMove = false;
        }
    }
}
