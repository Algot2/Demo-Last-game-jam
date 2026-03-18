using System;
using BoltsTools;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TMP_InputField sText, bText;
    public Slider sSlider, bSlider;

    public Image transition;
    
    public int savedSensitivity;
    
    public float savedBrightnes;
    
    public void LoadScene(int index)
    {
        Color endColor = Color.black;
        transition.DOColor(endColor, 1).OnComplete(() => SceneManager.LoadScene(index));
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
        int finalSensitivity = (int)Mathf.Lerp(1, 200, value);

        NewPlayerInput.globalSensitivity = (float)finalSensitivity;

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

    private void Start()
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

    public void Quit()
    {
        Application.Quit();
       
        Debug.Log("Quit");
    }

    public void ResetSave()
    {
        BoltsSave.ResetSave();
    }

    void Awake()
    {
        BoltsSave.Initialize();
    }
}
