using System;
using NaughtyAttributes;
using UnityEngine;
using UltEvents;

public class Trigger : MonoBehaviour
{
    public bool canTriger = true;
    public bool triggerOnce = true;

    [ShowIf("triggerOnce")] 
    public bool hasTriggered;
    
    [Tag]
    public string tagToDetect;
    
    [BoltsComment("When Entering Trigger", 10)]
    public UltEvent onEnter;

    [BoltsComment("When Inside Trigger, Called Every Frame", 10)]
    public UltEvent onTrigger;

    [BoltsComment("When Exiting Trigger", 10)]
    public UltEvent onExit;

    public bool isInside;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToDetect && !hasTriggered && canTriger)
        {
            onEnter.Invoke();

            isInside = true;

            hasTriggered = triggerOnce;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == tagToDetect && !hasTriggered && canTriger)
        {
            onExit.Invoke();

            isInside = false;

            hasTriggered = triggerOnce;
        }
    }

    private void Update()
    {
        if(isInside && canTriger)
            onTrigger.Invoke();
    }

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            if(!GameManager.Instance.triggers.Contains(this))
                GameManager.Instance.triggers.Add(this);
        }
    }
}
