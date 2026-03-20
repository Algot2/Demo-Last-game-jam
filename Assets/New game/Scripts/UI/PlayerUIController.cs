using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BoltsTools;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController Instance;

    public GameObject deathUIObj, pauseUIObj;
    public NewPlayerInput inputs;
    public List<GameObject> pauseUIs;

    public TMP_InputField sText, bText;
    public Slider sSlider, bSlider;

    public GameObject cheatVolume;
    
    public int savedSensitivity;

    public float savedBrightnes;
    
    public Image transition;

    public bool cheatActive;
    public float normalMaxHP;
    bool fading = false;
    public void PlayerDied()
    {
        deathUIObj.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        inputs.isDed = true;

        NewHuerBox.CanDamage = false;

        //Time.timeScale = 0;
    }

    public void Pause()
    {
        pauseUIObj.SetActive(true);
        
        for(int i = 1; i < pauseUIs.Count; i++)
            pauseUIs[i].SetActive(false);
        
        pauseUIs[0].SetActive(true);
        
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
    }
    
    public void LoadSave()
    {
        if (!fading)
        {
            fading = true;
            Color endColor = new(0, 0, 0, 1);
            transition.DOColor(endColor, 1).OnComplete(() =>
            {
                fading = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;

                deathUIObj.SetActive(false);
                
                Color startColor = new(0, 0, 0, 0);
                transition.DOColor(startColor, 1);

                CheckpointController.LoadGame();
            });
        }
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
    
    public void LoadObj(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void UnLoadObj(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void SetSensitivity(float value)
    {
        int finalSensitivity = (int)Mathf.Lerp(1, 350, value);

        NewPlayerInput.globalSensitivity = finalSensitivity;

        savedSensitivity = finalSensitivity;
        
        sText.text = $"{finalSensitivity}";
    }

    public void SetSensitivity(string value)
    {
        if(string.IsNullOrEmpty(value))
            return;
     
        float textToFloat = float.Parse(value);

        if (textToFloat > 200)
            textToFloat = 200;
        if (textToFloat < 1)
            textToFloat = 1;

        NewPlayerInput.globalSensitivity = textToFloat;

        savedSensitivity = (int)textToFloat;
        
        float sliderValue = Mathf.Lerp(0, 1, (textToFloat / 200));
        sSlider.value = sliderValue;
        
        sText.text = $"{textToFloat}";
    }

    public void SetBrightnes(float value)
    {
        int finalBrightnes = (int)Mathf.Lerp(0.5f, 5, value);

        NewPlayerInput.globalBrightnes = finalBrightnes;

        savedBrightnes = finalBrightnes;
        
        bText.text = $"{finalBrightnes}";
    }

    public void SetBrightnes(string value)
    {
        if(string.IsNullOrEmpty(value))
            return;
     
        float textToFloat = float.Parse(value);

        if (textToFloat > 5)
            textToFloat = 5;
        if (textToFloat < 0.5f)
            textToFloat = 0.5f;

        NewPlayerInput.globalBrightnes = textToFloat;

        savedBrightnes = textToFloat;
        
        float sliderValue = Mathf.Lerp(0, 1, (textToFloat / 5));
        bSlider.value = sliderValue;
        
        bText.text = $"{textToFloat}";
    }

    public void SaveSettings()
    {
        BoltsSave.SaveIntValue("Sensitivity", savedSensitivity);
        
        BoltsSave.SaveFloatValue("Brightnes", savedBrightnes);
    }

    public void ChatActive()
    {
        if (!cheatActive)
        {
            cheatVolume.SetActive(true);
            
            normalMaxHP = inputs.hellfSlider.max;
            inputs.hellfSlider.max = 1000;
            inputs.hellfSlider.setValu(1000);

            cheatActive = true;
        }
        else
        {
            cheatVolume.SetActive(false);
            
            inputs.hellfSlider.max = normalMaxHP;
            inputs.hellfSlider.setValu(normalMaxHP);

            cheatActive = false;
        }
    }
    
    public void ResumeGame()
    {
        pauseUIObj.SetActive(false);
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        transition.color = Color.black;

        Color endColor = new(0, 0, 0, 0);
        transition.DOColor(endColor, 1);
        
        int getSensitivity = BoltsSave.GetInt("Sensitivity");

        float getBrightnes = BoltsSave.GetFloat("Brightnes");

        if (getSensitivity == -1)
            getSensitivity = 100;

        savedSensitivity = getSensitivity;

        if (getBrightnes == -1)
            getBrightnes = 2;

        savedBrightnes = getBrightnes;
        
        sSlider.value = Mathf.Lerp(0, 1, ((float)getSensitivity / 200));
        sText.text = $"{getSensitivity}";
        
        bSlider.value = Mathf.Lerp(0.5f, 5, getBrightnes / 5);
        bText.text = $"{getBrightnes}";
        
        BoltsCommands.command.AddCommand("cheat", "ChatActive", this);
    }

    void Update()
    {
        if (inputs.isPaused)
        {
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.T))
                ChatActive();
        }
    }
}
