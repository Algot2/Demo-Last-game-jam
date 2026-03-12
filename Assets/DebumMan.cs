using UnityEngine;

public class DebumMan : MonoBehaviour
{
    public Vector3 basePos, Ofset;
    public CheckpointController[] SetNewChekpont;
    public int teleportCont;
    bool active = false;

    void teleport(int teleportspot) {
        CheckpointController.TeleportPlayer(SetNewChekpont[teleportspot].transform.position + Vector3.up*2);
        SetNewChekpont[teleportspot].SaveGame();
    }
    private void OnGUI() {
        if (active)
        {
            for (int i = 0; i < teleportCont; i++)
                if (GUI.Button(new Rect
                {
                    position = basePos + Ofset * i,
                    width = Ofset.magnitude * 2,
                    height = Ofset.magnitude
                }, $"Teleport : {i}"))
                {
                    teleport(i);
                }

            GUI.TextArea(new Rect
            {
                position = basePos - Ofset,
                width = 50f,
                height = 25f
            }, $"fps {Mathf.Round(1f / Time.deltaTime)}");
        }
    }
    void Update() {

        if (Input.GetKeyDown(KeyCode.F3))  {
            active = !active;
            if (!active) Cursor.lockState = CursorLockMode.Locked;
        }

        if (Cursor.lockState == CursorLockMode.Locked && active)
            Cursor.lockState = CursorLockMode.Confined;
    }
}
