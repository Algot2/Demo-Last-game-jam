using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BaseEnemyLogic : MonoBehaviour
{
    public HellfSlider health;
    public GameObject hurtBox;
    public Rigidbody rb;
    
    public float timeTillDeath;
    
    public List<EnemyMaterialSettings> deathSettings;

    public SpawnEnemiesTrigger owner;
    public Material mat;
    public bool isDead;


    private void OnDestroy()
    {
        GameManager.Instance.enemies.Remove(this);
    }
    void Update() {
            
        if (health.curnt <= 0 && !isDead) {
            hurtBox.SetActive(false);

            if (owner != null) {
                owner.enemiesAlive--;
                
                if(owner.enemiesAlive == 0)
                    owner.afterEnemiesDead.Invoke();
            }
            
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
            if(!GameManager.Instance.enemies.Contains(this))
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
    public string obj;

    public Vector3 pos;
    
    public float current;
}