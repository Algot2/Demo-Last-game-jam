using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class atack {
    public GameObject HurtBox;
    public float time;
    public int AtackAnimasen;
}
public class PlAtackMan : MonoBehaviour
{
    [SerializeField]
    public List<atack> atacks = new List<atack>();
    public HellfSlider HellfSlider;
    public PlMoment PlMoment;
    public Animator Animator;
    public void PreformAtack(int i, float time) {
            StartCoroutine(Timer.RunAfterTimer(0.5f, 
                () => StartCoroutine(Timer.StartTimer(atacks[i].time, (f) => atacks[i].HurtBox.SetActive(f)))));

           
            StartCoroutine(Timer.RunAfterTimer(0.5f, 
                () => StartCoroutine(Timer.StartTimer(0.5f, (f) => HellfSlider.Inmune = f))));
            Animator.SetTrigger("Atck");

            //float sped = PlMoment.Sped;
            //PlMoment.Sped = 0;
           // StartCoroutine(Timer.RunAfterTimer(atacks[i].time, () => PlMoment.Sped = sped));
    }
}