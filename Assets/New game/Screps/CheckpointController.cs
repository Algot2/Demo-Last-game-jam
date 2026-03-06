using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public SpawnEnemiesTrigger spawner;
    
    [BoltsSave(SavedVariableType.Vector3)]
    public string positionString;

    public static string staticPositionString;

    public GameObject Efects;
    public bool StartingStetpont;

    void Start()
    {
        if (StartingStetpont)
            SaveGame();
    }
    public void SaveGame()
    {
        bool runSave = true;
        
        if(spawner != null)
        {
            runSave = spawner.hasTriggered && spawner.enemiesAlive == 0;
        }

        if (runSave)
        {
            Efects.SetActive(true);
            GameManager.chekpont = gameObject;
            StartCoroutine(Timer.RunAfterCondishen(() => { Efects.SetActive(false); },
                () => GameManager.chekpont != gameObject));

            Debug.Log("Saved Game");

            // Saves The Player Position
            BoltsSave.SaveVector3Value(positionString, GameManager.player.position);

            // Sets Players HP To Full
            GameManager.player.GetComponent<PlMoment>().HellfSlider.setValu(
                GameManager.player.GetComponent<PlMoment>().HellfSlider.max);

            GetComponent<Trigger>().hasTriggered = true;

            BoltsSave.ResetAllBoolsWithName("Trigger: Index = (");

            // Saves All Trigger States
            List<Trigger> triggers = GameManager.Instance.triggers;
            for (int i = 0; i < triggers.Count; i++)
            {
                string boolName = $"Trigger: Index = ({i})";
                BoltsSave.SaveBoolValue(boolName, triggers[i].hasTriggered);
            }

            // Old Stuff
            /*
            // Resets The List So It Can Be Used Again
            BoltsSave.ResetSavedClassesWithName("Enemy: Index = (");

            // Saves All Enemies
            List<BaseEnemyLogic> enemies = GameManager.Instance.enemies;

            for (int i = 0; i < enemies.Count; i++)
            {
                string enemyName = enemies[i].gameObject.name;
                string[] nameSplit = enemyName.Split(" (");
                enemyName = nameSplit[0];

                SavedEnemy newSavedEnemy = new()
                { obj = enemyName, current = enemies[i].health.curnt, pos = enemies[i].transform.position };
                BoltsSave.SaveClassVariable($"Enemy: Index = ({i})", newSavedEnemy);
            }*/

            // Save Changes
        }
    }

    public static void LoadGame()
    {
        GameManager.player.GetComponent<PlMoment>().HellfSlider.setValu(
            GameManager.player.GetComponent<PlMoment>().HellfSlider.max);
        
        // Sets The Player Position To The Saved One
        Vector3 savedPos = BoltsSave.GetVector3(staticPositionString);
        GameManager.player.position = savedPos;
        DragonAI.Instens.transform.position = savedPos;
        
        // Loads All Trigger States
        List<Trigger> triggers = GameManager.Instance.triggers;
        List<SaveBool> allBools = BoltsSave.GetAllBools();
        for (int i = 0; i < triggers.Count; i++)
        {
            triggers[i].hasTriggered = allBools[i].value;

            if (triggers[i] is SpawnEnemiesTrigger)
            {
                SpawnEnemiesTrigger spawner = triggers[i] as SpawnEnemiesTrigger;
                if (spawner.hasSpawnedEnemies && spawner.enemiesAlive > 0)
                {
                    spawner.enemiesAlive = 0;
                    spawner.hasSpawnedEnemies = false;
                    spawner.hasTriggered = false;
                }
            }
        }

        for (int i = 0; i < GameManager.Instance.enemies.Count; i++)
        {
            Destroy(GameManager.Instance.enemies[i].gameObject);
        }
        
        GameManager.Instance.enemies.Clear();
        
        // Loads All Enemies
        //List<string> savedEnemyClassNames = BoltsSave.GetAllClassesNameWithName("Enemy: Index = (");
        //foreach (var e in savedEnemyClassNames)
        //{
        //    SavedEnemy newEnemy = BoltsSave.LoadClass<SavedEnemy>(e);
        //    GameObject newEnemyObj = Instantiate(Resources.Load<GameObject>(newEnemy.obj), newEnemy.pos, Quaternion.identity);
        //    newEnemyObj.GetComponent<BaseEnemyLogic>().health.setValu(newEnemy.current);
        //}

        // Load Changes
    }

    public static void TeleportPlayer(Vector3 pos)
    {
        DragonAI.Instens.transform.position = pos;
        
        GameManager.player.GetComponent<CharacterController>().enabled = false;
        GameManager.player.position = pos;
        GameManager.player.GetComponent<CharacterController>().enabled = true;
    }
    
    public static void TeleportPlayer(Vector3 pos, bool disableSnow)
    {
        DragonAI.Instens.transform.position = pos;
        
        GameManager.player.GetComponent<CharacterController>().enabled = false;
        GameManager.player.position = pos;
        GameManager.player.GetComponent<CharacterController>().enabled = true;
        
        GameManager.player.GetComponent<NewPlayerInput>().snow.SetActive(disableSnow);
    }

    private void Awake()
    {
        staticPositionString = positionString;
    }
}
