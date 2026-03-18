using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HellfSlider : MonoBehaviour
{
    public float max, curnt;
    public Slider valu, frontjump;
    public bool ImuneSwitsh;
    public int imune;
    public bool IsPlayer;
    public Action Onhit = () => { };

    private void Start()
    {
        valu.maxValue = max;
        frontjump.maxValue = max;

        valu.value = curnt;
        frontjump.value = curnt;
    }
    public void setValu(float val)
    {
        Onhit();
        if (!ImuneSwitsh) {
            curnt = val;
            frontjump.value = val;
        }

        if (IsPlayer && curnt <= 0) PlayerUIController.Instance.PlayerDied();

        if (IsPlayer && ImuneSwitsh) {
            StartCoroutine(Timer.RunAfterTimer(0.1f, () => imune--));
            StartCoroutine(Timer.StartTimer(5, (f) => NewPlayerInput.Instance.canDo[3] = !f)); 
        }

        curnt = Mathf.Clamp(curnt, -1, max);
    }

    void Update() {
        ImuneSwitsh = imune > 0;
        if (imune < 0) imune = 0;

        transform.LookAt(Camera.main.transform);
        transform.forward = -transform.forward;
        valu.value = Mathf.Lerp(valu.value, curnt, Time.deltaTime * 10);
    }

    [Button]
    void KillPlayer()
    {
        setValu(0);
    }
}