using UnityEngine;

public class LOD : MonoBehaviour
{
    public GameObject[] LODLevels;
    public float[] Dist;
    public int Lev = 0;
    public bool randomise;

    void Start() {
        if (randomise) {
            Vector3 S = transform.localScale;
            transform.localScale = new Vector3 { 
                x = S.x * Random.Range(0.9f,1.1f),
                y = S.y * Random.Range(0.9f,1.1f),
                z = S.z * Random.Range(0.9f,1.1f)
            } * Random.Range(0.8f, 1.2f);

            transform.rotation *= Quaternion.Euler(0, Random.Range(-180, 180), 0);
        }
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
