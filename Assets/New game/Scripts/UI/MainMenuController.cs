using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TMP_InputField sText;
    public Slider sSlider;

    public Image transition;
    
    public int savedSensitivity;
    
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

    public void SaveSettings()
    {
        BoltsSave.SaveIntValue("Sensitivity", savedSensitivity);
    }

    private void Start()
    {
        transition.color = Color.black;

        Color endColor = new(0, 0, 0, 0);
        transition.DOColor(endColor, 1);
        
        int getSensitivity = BoltsSave.GetInt("Sensitivity");

        if (getSensitivity == -1)
            getSensitivity = 100;

        savedSensitivity = getSensitivity;

        sSlider.value = Mathf.Lerp(0, 1, ((float)getSensitivity / 200));
        sText.text = $"{getSensitivity}";
    }

    public void Quit()
    {
        Application.Quit();
       
        Debug.Log("Quit");
    }
}
