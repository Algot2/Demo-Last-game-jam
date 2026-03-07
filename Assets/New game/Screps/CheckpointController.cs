using System;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public SpawnEnemiesTrigger spawner;
    
    [BoltsSave(SavedVariableType.Vector3)]
    public string positionString;

    public static string staticPositionString;

    public GameObject Efects;
    public bool StartingStetpont;

    public UltEvent onLoadGame;
    public static UltEvent staticOnLoadGame;
    
    void Start()
    {
        if (StartingStetpont)
            SaveGame();
        
        onLoadGame.Invoke();
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
        NewPlayerInput plIn = GameManager.player.GetComponent<NewPlayerInput>();
        plIn.isDed = false;
        plIn.animator.SetBool("Ded", false);

        GameManager.player.GetComponent<PlMoment>().HellfSlider.setValu(
            GameManager.player.GetComponent<PlMoment>().HellfSlider.max);
        
        // Sets The Player Position To The Saved One
        Vector3 savedPos = BoltsSave.GetVector3(staticPositionString);
        GameManager.player.position = savedPos;
        DragonAI.Instens.gameObject.SetActive(false);
        DragonAI.Instens.transform.position = savedPos;
        DragonAI.Instens.gameObject.SetActive(true);

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
        
        staticOnLoadGame.Invoke();
        
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
        Destroy(DragonAI.Instens.gameObject);

        Instantiate(GameManager.Instance.dragon, pos, Quaternion.identity);


        GameManager.player.GetComponent<CharacterController>().enabled = false;
        GameManager.player.position = pos;
        GameManager.player.GetComponent<CharacterController>().enabled = true;
    }
    
    public static void TeleportPlayer(Vector3 pos, bool showSnow)
    {
        Destroy(DragonAI.Instens.gameObject);

        Instantiate(GameManager.Instance.dragon, pos, Quaternion.identity);

        GameManager.player.GetComponent<CharacterController>().enabled = false;
        GameManager.player.position = pos;
        GameManager.player.GetComponent<CharacterController>().enabled = true;

        if (GameManager.player.GetComponent<NewPlayerInput>().snow != null)
        {
            if (showSnow)
                GameManager.player.GetComponent<NewPlayerInput>().snow.Play();
            else
                GameManager.player.GetComponent<NewPlayerInput>().snow.Stop();
        }
    }

    void Awake()
    {
        staticPositionString = positionString;

        staticOnLoadGame = onLoadGame;
    }

    public void SpawnBoos(GameObject boos, Transform position)
    {
        Instantiate(boos, position.position, Quaternion.identity);
    }
}
