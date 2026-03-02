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

    public bool isDead;

    private void Update()
    {
        if (health.curnt <= 0 && !isDead)
        {

            StartCoroutine(Timer.RunAfterTimer(timeTillDeath + 0.1f, () =>
            {
                GameManager.Instance.enemies.Remove(this);
                Destroy(gameObject);
            }));
            List<Material> materials = new List<Material>();

         

            for (int i = 0; i < deathSettings.Count; i++) {
                float end = deathSettings[i].endValue;
                string cutOff = deathSettings[i].cutOff;
                for (int j = 0; j < deathSettings[i].renderer.materials.Length; j++) {
                    materials.Add(Instantiate(deathSettings[i].materialToChangeTo));
                    deathSettings[i].renderer.materials = materials.ToArray();
                    deathSettings[i].renderer.materials[j].DOFloat(end, cutOff, timeTillDeath);
                }
            }

            isDead = true;
        }
    }

    private void OnValidate()
    {
        if (rb == null)
            rb = GetComponentInChildren<Rigidbody>();

        if (health == null)
            health = GetComponentInChildren<HellfSlider>();
    }

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            int index = GameManager.Instance.enemies.FindIndex(x => x = this);
            if(index == -1)
                GameManager.Instance.enemies.Add(this);
        }
    }
}

[Serializable]
public class EnemyMaterialSettings
{
    public Renderer renderer;

    public float endValue;
    
    public Material materialToChangeTo;

    [BoltsShaderProperty("materialToChangeTo")]
    public string cutOff;
}

[Serializable]
public class SavedEnemy
{
    public GameObject obj;

    public Vector3 pos;
    
    public float current;
}