using System;
using NaughtyAttributes;
using UnityEngine;
using UltEvents;

public class Trigger : MonoBehaviour
{
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
        if (other.tag == tagToDetect)
        {
            onEnter.Invoke();

            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == tagToDetect)
        {
            onExit.Invoke();

            isInside = false;
        }
    }

    private void Update()
    {
        if(isInside)
            onTrigger.Invoke();
    }
}
