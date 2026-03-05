using UnityEngine;

public class LOD : MonoBehaviour
{
    public GameObject[] LODLevels;
    public float[] Dist;
    public int Lev = 0;
    public bool randomiseSise;
    public Vector3 min;
    public Vector3 Max;

    void Start() {
       if( randomiseSise )transform.lossyScal  
    }
    void Update() {
        float dis = Vector3.Distance(transform.position, Camera.main.transform.position);

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
