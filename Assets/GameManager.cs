using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float minDist = 5;
    
    public Transform PL;
    public static Transform player;

    public List<BaseEnemyLogic> enemies;

    public List<Trigger> triggers;
    
    private void Awake()
    {
        Instance = this;
        player = PL;
    }

    private void Update()
    {
        for (int i = 0; i < enemies.Count; i++) // Check Every Enemy
        {
            for (int x = 0; x > enemies.Count; x++) // Check If Its To Close
            {
                Vector3 dist = enemies[x].transform.position - enemies[i].transform.position;
                
                if (dist.magnitude < minDist)
                {
                    enemies[i].rb.AddForce(Vector3.one * minDist - dist);
                    enemies[x].rb.AddForce(-(Vector3.one * minDist - dist));
                }
            }
        }
    }
}
