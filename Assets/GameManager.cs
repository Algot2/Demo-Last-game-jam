using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject dragon;
    public LayerMask ground;
    public float minDist = 5;
    public Material BaseShader;
    public Color FogStartColor;
    
    public Transform PL;
    public static Transform player;

    public List<BaseEnemyLogic> enemies;

    public List<Trigger> triggers;
    public static GameObject chekpont ;

 
    void Awake()
    {
        chekpont = gameObject;
        Instance = this;
        player = PL;
        BaseShader.SetColor("_FongColer", FogStartColor);

    }

    //void Update()
    //{
    //    for (int i = 0; i < enemies.Count; i++) // Check Every Enemy
    //    {
    //        //if (enemies[i].IsUnityNull()) 
    //        //    enemies.RemoveAt(i);

    //        for (int x = 0; x < enemies.Count; x++) // Check If Its To Close
    //        {
    //            Vector3 dist = enemies[x].transform.position - enemies[i].transform.position;
    //            if (dist.magnitude < minDist)
    //            {
    //                //enemies[i].rb.AddForce(-(Vector3.one * minDist - dist));
    //                //enemies[x].rb.AddForce((Vector3.one * minDist - dist));
    //            }
    //        }
    //    }
    //}
}
