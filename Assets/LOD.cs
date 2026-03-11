using UnityEngine;

public class LOD : MonoBehaviour
{
    public GameObject[] LODLevels;
    public float[] Dist;
    public int Lev = 0;
    public bool randomise;
    public bool ForeTranssform;

    private void Awake() {
        LODGruper.LODs.Add(this);
    }
    void Start() {
        if (ForeTranssform) {
            LODLevels[1].transform.position = LODLevels[0].transform.position;
            LODLevels[1].transform.rotation = LODLevels[0].transform.rotation;
            LODLevels[1].transform.localScale = LODLevels[0].transform.localScale;
        }
    }
    void Update() {
        Vector3 difrens = LODLevels[0].transform.position - Camera.main.transform.position;
        float dis = difrens.magnitude;

        int lev = 0;
        foreach (float D in Dist) {
            if (D < dis) lev++;
        }

        lev = Mathf.Clamp(lev, 0, Dist.Length-1);
        

        if (lev != Lev) {
            LODLevels[Lev].SetActive(false);
            LODLevels[lev].SetActive(true);
            Lev = lev;
        }

        if (lev != Dist.Length - 1)
        {
            float dotProdoct = Vector3.Dot(difrens.normalized, Camera.main.transform.forward);
            LODLevels[Lev].SetActive(dotProdoct > 0.2f);
        }

    }
}
