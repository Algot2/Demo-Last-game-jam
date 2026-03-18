using System;
using System.Collections.Generic;
using BoltsTools;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<CheckpointController> chekponts;
    public GameObject dragon;
    public LayerMask ground;
    public float minDist = 5;
    public Material BaseShader;
    public Color FogStartColor;
    public Color CaveColor;

    public Transform PL;
    public static Transform player;

    public List<BaseEnemyLogic> enemies;

    public List<Trigger> triggers;
    public static GameObject chekpont ;

 
    void Awake()
    {
        BoltsSave.Initialize();
        
        chekpont = gameObject;
        Instance = this;
        player = PL;
        BaseShader.SetColor("_FongColer", FogStartColor);
      
    }

    void Start()
    {
        BoltsCommands.command.AddCommand("kill", "KillEnemies", this);
        BoltsCommands.command.AddCommand("trigger", "TriggerAllEnemies", this);
    }

    public void KillEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].health.Inmune = false;
            enemies[i].health.setValu(0);
        }
    }

    public void TriggerAllEnemies()
    {
        for (int i = 0; i < triggers.Count; i++)
        {
            if (triggers[i] is SpawnEnemiesTrigger)
            {
                triggers[i].hasTriggered = triggers[i].triggerOnce;
                SpawnEnemiesTrigger spawner = triggers[i] as SpawnEnemiesTrigger;
                spawner.SpawnEnemies();
            }
        }
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
