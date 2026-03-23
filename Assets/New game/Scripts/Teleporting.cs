using UnityEngine;

public class TeleportingScre : MonoBehaviour
{
    public Transform destnation;
    public GameObject[] AnabolDiabol;
    public bool setFogColr;
    public bool Otside;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (setFogColr)
            {
                GameManager.Instance.BaseShader.SetColor("_FongColer", Otside ? GameManager.Instance.FogStartColor : GameManager.Instance.CaveColor);
            }
            CheckpointController.TeleportPlayer(destnation.position);

            foreach (GameObject G in AnabolDiabol)
                G.SetActive(!G.activeSelf);
        }
    }
}
