using System;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemiesTrigger : Trigger
{
    [BoltsComment("When All Enemies Are Dead")]
    public UltEvent afterEnemiesDead;
    
    public EnemiesSpawnSettings spawnSettings;

    public int enemiesAlive = -1;

    public bool hasSpawnedEnemies;
    
    public void SpawnEnemies()
    {
        hasSpawnedEnemies = true;
        
        Vector3 center = transform.position + transform.TransformDirection(spawnSettings.offset);

        float minX = center.x - (spawnSettings.bounds.x / 2);
        float maxX = center.x + (spawnSettings.bounds.x / 2);

        float minZ = center.z - (spawnSettings.bounds.y / 2);
        float maxZ = center.z + (spawnSettings.bounds.y / 2);

        for (int i = 0; i < spawnSettings.enemiesToSpawn.Count; i++)
        {
            for (int x = 0; x < spawnSettings.enemiesToSpawn[i].count; x++)
            {
                Vector3 spawnPos = new(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));

                if (Physics.Raycast(spawnPos, Vector3.down, out var hit))
                    spawnPos.y = hit.point.y;

                BaseEnemyLogic newEnemy =
                    Instantiate(spawnSettings.enemiesToSpawn[i].enemy, spawnPos, Quaternion.identity)
                        .GetComponent<BaseEnemyLogic>();
                newEnemy.owner = this;

                enemiesAlive++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 center = transform.position + transform.TransformDirection(spawnSettings.offset);
        
        //Define The Fore Corners In Local Space
        Vector3 bottomLeftOffset = new Vector3(-spawnSettings.bounds.x / 2, 0, -spawnSettings.bounds.y / 2);
        Vector3 bottomRightOffset = new Vector3(spawnSettings.bounds.x / 2, 0, -spawnSettings.bounds.y / 2);
        Vector3 topRightOffset = new Vector3(spawnSettings.bounds.x / 2, 0, spawnSettings.bounds.y / 2);
        Vector3 topLeftOffset = new Vector3(-spawnSettings.bounds.x / 2, 0, spawnSettings.bounds.y / 2);

        //Rotate To Face The Correct Direction
        Vector3 bottomLeft = center + transform.TransformDirection(bottomLeftOffset);
        Vector3 bottomRight = center + transform.TransformDirection(bottomRightOffset);
        Vector3 topRight = center + transform.TransformDirection(topRightOffset);
        Vector3 topLeft = center + transform.TransformDirection(topLeftOffset);
        
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}

[Serializable]
public class EnemiesSpawnSettings
{
    public Vector2 bounds = Vector2.one;
    public Vector3 offset;

    public List<SpawnEnemySetting> enemiesToSpawn;
}

[Serializable]
public class SpawnEnemySetting
{
    public GameObject enemy;
    public int count;
}