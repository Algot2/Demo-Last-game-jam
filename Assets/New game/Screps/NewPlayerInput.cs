using UnityEngine;

public class NewPlayerInput : MonoBehaviour
{
    public CamraMoment cam;
    public PlMoment plMoment;
    public PlAtackMan plAtacks;
    public float sensetivety;
    void Update() {
        Cursor.lockState = CursorLockMode.Locked;
        plMoment.Move(new Vector3(Input.GetAxisRaw("H"), 0, Input.GetAxisRaw("V")));
        cam.setCamraDireksen(new Vector2(-Input.mousePositionDelta.x / Screen.width, Input.mousePositionDelta.y / Screen.height) * sensetivety);
        if (Input.GetMouseButtonDown(0)) plAtacks.PreformAtack(0, 2);

    }
}
