using UnityEngine;

public class LOD : MonoBehaviour
{
    public GameObject[] LODLevels;
    public float[] Dist;
    public int Lev = 0;
    public bool randomise;
    public bool ForeTranssform;

    void Start() {
        if (ForeTranssform) {
            LODLevels[1].transform.position = LODLevels[0].transform.position;
            LODLevels[1].transform.rotation = LODLevels[0].transform.rotation;
            LODLevels[1].transform.localScale = LODLevels[0].transform.localScale;
        }
    }
    void Update() {
        float dis = Vector3.Distance(LODLevels[0].transform.position, Camera.main.transform.position);

        int lev = 0;
        foreach (float D in Dist) {
            if (D < dis) lev++;
        }

        lev = Mathf.Clamp(lev, 0, Dist.Length-1);

        if (lev != Lev)
        {
            LODLevels[Lev].SetActive(false);
            LODLevels[lev].SetActive(true);
            Lev = lev;
        }

    }
}
