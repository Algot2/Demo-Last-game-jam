using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController Instance;
    
    public GameObject deathUIObj;

    public Image transition;

    public void PlayerDied()
    {
        deathUIObj.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        
        Time.timeScale = 0;
    }
    
    public void LoadSave()
    {
        Color endColor = new(0, 0, 0, 1);
        transition.DOColor(endColor, 1).OnComplete(() =>
        {
            Time.timeScale = 1;
        
            deathUIObj.SetActive(false);
            CheckpointController.LoadGame();
            
            Color startColor = new(0, 0, 0, 0);
            transition.DOColor(startColor, 1);
        });
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        
        Application.Quit();
       
        Debug.Log("Quit");
    }

    public void LoadScene(int index)
    {
        Color endColor = new(0, 0, 0, 1);
        transition.DOColor(endColor, 1).OnComplete(() =>
        {
            Time.timeScale = 1;

            SceneManager.LoadScene(index);
        });
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        transition.color = Color.black;

        Color endColor = new(0, 0, 0, 0);
        transition.DOColor(endColor, 1);
    }
}
