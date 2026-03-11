using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LODGruper : MonoBehaviour {
    public static List<LOD> LODs = new();
    public float GrupeDimensen;
    public float WorldScale;
    public float GrupeUnlodeDistens;
    public Transform[,] Chunks = new Transform[1,1];
    public List<Transform> chunks;
    void Start() {
        int n = Mathf.RoundToInt(WorldScale / GrupeDimensen);
        Chunks = new Transform[n, n];
        //foreach (Transform T in Chunks) {
        //   Destroy(T.gameObject);
        //}
        foreach (LOD LOD in LODs) {
            Transform T = LOD.transform;
            Vector3 pos = T.position;
            int x = (int)(pos.x / GrupeDimensen);
            int y = (int)(pos.z / GrupeDimensen);

            if (Chunks[x, y].IsUnityNull()) {
                Chunks[x, y] = Instantiate(new GameObject(), (new Vector3(x, 0, y) * GrupeDimensen), Quaternion.identity).transform;
                Chunks[x, y].SetParent(transform);
                chunks.Add(Chunks[x, y]);
            }
            T.SetParent(Chunks[x, y]);
        }

        Chunks = new Transform[0, 0];
    }

    void Update() {
        Vector2 camPos = new Vector2 { 
            x = Camera.main.transform.position.x,
            y = Camera.main.transform.position.z
        };

        foreach (Transform T in chunks) {
            Vector2 ChunkPos = new Vector2
            {
                x = T.position.x,
                y = T.position.z
            };
            Vector2 dis = ChunkPos - camPos;
            T.gameObject.SetActive(dis.magnitude < GrupeUnlodeDistens);
        }
    }
}