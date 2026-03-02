using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BaseEnemyLogic : MonoBehaviour
{
    public HellfSlider health;

    public Rigidbody rb;
    
    public float timeTillDeath;
    
    public List<EnemyMaterialSettings> deathSettings;

    private void Update()
    {
        if (health.curnt <= 0)
        {
            for (int i = 0; i < deathSettings.Count; i++)
            {
                int matIndex = deathSettings[i].matIndex;
                float end = deathSettings[i].endValue;
                string cutOff = deathSettings[i].cutOff;

                deathSettings[i].renderer.materials[matIndex].DOFloat(end, cutOff, timeTillDeath).OnComplete(() =>
                {
                    EnemyMansher.Instance.enemies.Remove(this);
                    
                    Destroy(gameObject);
                });
            }
        }
    }

    private void OnValidate()
    {
        if (rb == null)
        {
            rb = GetComponentInChildren<Rigidbody>();
        }
    }
}

[Serializable]
public class EnemyMaterialSettings
{
    public int matIndex;
    public Renderer renderer;

    public float endValue;
    
    public Material materialToChangeTo;

    [BoltsShaderProperty("materialToChangeTo")]
    public string cutOff;
}