using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HellfSlider : MonoBehaviour
{
    public float max, curnt;
    public Slider valu, frontjump;
    public bool Inmune;
    public bool IsPlayer;

    private void Start()
    {
        valu.maxValue = max;
        frontjump.maxValue = max;

        valu.value = curnt;
        frontjump.value = curnt;
    }
    public void setValu(float val)
    {
        if (!Inmune) {
            curnt = val;
            frontjump.value = val;
        }

        if (IsPlayer && curnt <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (IsPlayer && Inmune) {
            StartCoroutine(Timer.RunAfterTimer(0.5f, () => Inmune = false));
            StartCoroutine(Timer.StartTimer(5, (f) => NewPlayerInput.Instance.canDo[3] = !f)); 
        }
    }

    void Update() {

        transform.LookAt(Camera.main.transform);
        transform.forward = -transform.forward;
        valu.value = Mathf.Lerp(valu.value, curnt, Time.deltaTime * 10);
    }
}