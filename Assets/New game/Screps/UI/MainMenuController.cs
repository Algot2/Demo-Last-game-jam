using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TMP_InputField sText;
    public Slider sSlider;
    
    public void LoadScene(int index)
    {
        // Play Transition Before
        SceneManager.LoadScene(index);
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

        sText.text = $"{finalSensitivity}";
    }

    public void SetSensitivity(string value)
    {
        float textToInt = float.Parse(value);

        if (textToInt > 200)
            textToInt = 200;
        if (textToInt < 1)
            textToInt = 1;
        
        float sliderValue = Mathf.Lerp(0, 1, (textToInt / 200));
        sSlider.value = sliderValue;
        
        sText.text = $"{textToInt}";
    }

    public void Quit()
    {
        Application.Quit();
       
        Debug.Log("Quit");
    }
}
