using UnityEngine;

public class LOD : MonoBehaviour
{
    public GameObject[] LODLevels;
    public float[] Dist;
    public int Lev = 0;

    void Update() {
        float dis = Vector3.Distance(transform.position, Camera.main.transform.position);

        int lev = 0;
        foreach (float D in Dist) {
            if (D < dis) lev++;
        }

        if (lev != Lev)
        {
            LODLevels[Lev].SetActive(false);
            LODLevels[lev].SetActive(true);
            Lev = lev;
        }

    }
}
