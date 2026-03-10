using UnityEngine;

public class TeleportingScre : MonoBehaviour
{
    public Transform destnation;
    public GameObject[] AnabolDiabol;
    public Color FogColor;
    public bool setFogColr;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (setFogColr)
            {
                GameManager.Instance.BaseShader.SetColor("_FongColer", FogColor);
            }
            CheckpointController.TeleportPlayer(destnation.position);

            foreach (GameObject G in AnabolDiabol)
                G.SetActive(!G.active);
        }
    }
}
