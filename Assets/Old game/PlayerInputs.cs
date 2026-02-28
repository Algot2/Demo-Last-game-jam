using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public PlMoment PlMoment;
    public CamraMoment CamraMoment;
    public PlAtacks PlAtacks;
    public float sensetivety;

    void Update() {
        Cursor.lockState = CursorLockMode.Locked;
        PlMoment.Move(new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")));
        CamraMoment.setCamraDireksen(new Vector2(-Input.mousePositionDelta.x/Screen.width, Input.mousePositionDelta.y/Screen.height) * sensetivety);
        
        if (Input.GetMouseButtonDown(0)) {
            PlAtacks.atack(0, 0.25f);
        }
    }
}
