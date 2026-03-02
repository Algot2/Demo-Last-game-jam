using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [BoltsSave(SavedVariableType.Vector3)]
    public string positionString;
    
    public void SaveGame()
    {
        // Saves The Player Position
        BoltsSave.SaveVector3Value(positionString, GameManager.player.position);

        // Sets Players HP To Full
        GameManager.player.GetComponent<PlMoment>().HellfSlider.curnt =
            GameManager.player.GetComponent<PlMoment>().HellfSlider.max;
        
        // Saves All Trigger States
        List<Trigger> triggers = GameManager.Instance.triggers;
        for (int i = 0; i < triggers.Count; i++)
        {
            string boolName = $"Trigger: Index = ({i})";
            BoltsSave.SaveBoolValue(boolName, triggers[i].hasTriggered);
        }

        // Saves All Enemies
        List<BaseEnemyLogic> enemies = GameManager.Instance.enemies;
        
        for (int i = 0; i < enemies.Count; i++)
        {
            SavedEnemy newSavedEnemy = new()
                { obj = enemies[i].gameObject, current = enemies[i].health.curnt, pos = enemies[i].transform.position };
            BoltsSave.SaveClassVariable($"Enemy: Index = ({i})", newSavedEnemy);
        }
        
        // Save Changes
    }

    public void LoadGame()
    {
        // Sets The Player Position To The Saved One
        GameManager.player.position = BoltsSave.GetVector3(positionString);
        
        // Loads All Trigger States
        List<Trigger> triggers = GameManager.Instance.triggers;
        List<SaveBool> allBools = BoltsSave.GetAllBools();
        for (int i = 0; i < triggers.Count; i++)
        {
            for (int x = 0; x < allBools.Count && allBools[x].name == $"Trigger: Index = ({i})"; x++)
            {
                triggers[i].hasTriggered = allBools[x].value;
            }
        }
        
        // Loads All Enemies
        List<string> savedEnemyClassNames = BoltsSave.GetAllClassesNameWithName("Enemy: Index = (");
        foreach (var e in savedEnemyClassNames)
        {
            SavedEnemy newEnemy = BoltsSave.LoadClass<SavedEnemy>(e);
            GameObject newEnemyObj = Instantiate(newEnemy.obj, newEnemy.pos, Quaternion.identity);
            newEnemyObj.GetComponent<BaseEnemyLogic>().health.setValu(newEnemy.current);
        }
        
        // Resets The List So It Can Be Used Again
        BoltsSave.ResetSavedClassesWithName("Enemy: Index = (");

        // Load Changes
    }
}
