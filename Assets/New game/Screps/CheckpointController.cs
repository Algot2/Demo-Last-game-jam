using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [BoltsSave(SavedVariableType.Vector3)]
    public string positionString;
    
    public void SaveGame()
    {
        BoltsSave.SaveVector3Value(positionString, GameManager.player.position);

        List<Trigger> triggers = GameManager.Instance.triggers;

        for (int i = 0; i < triggers.Count; i++)
        {
            string boolName = $"Trigger: Index = ({i})";
            BoltsSave.SaveBoolValue(boolName, triggers[i].hasTriggered);
        }
        
        Debug.Log("Saved Position");
        
        // Save Enemies Killed
        // Save Changes
    }

    public void LoadGame()
    {
        GameManager.player.position = BoltsSave.GetVector3(positionString);
        GameManager.player.GetComponent<PlMoment>().HellfSlider.curnt =
            GameManager.player.GetComponent<PlMoment>().HellfSlider.max;
        
        List<Trigger> triggers = GameManager.Instance.triggers;
        List<SaveBool> allBools = BoltsSave.GetAllBools();
        
        for (int i = 0; i < triggers.Count; i++)
        {
            for (int x = 0; x < allBools.Count && allBools[x].name == $"Trigger: Index = ({i})"; x++)
            {
                triggers[i].hasTriggered = allBools[x].value;
            }
        }
        
        // Load Dead Enemies
        // Load Changes
    }
}
