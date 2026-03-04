using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController Instance;
    
    public GameObject deathUIObj;

    public void PlayerDied()
    {
        deathUIObj.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void LoadSave()
    {
        Time.timeScale = 1;
        CheckpointController.LoadGame();
        
        deathUIObj.SetActive(false);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        
        Application.Quit();
       
        Debug.Log("Quit");
    }

    public void LoadScene(int index)
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(index);
    }

    private void Awake()
    {
        Instance = this;
    }
}
