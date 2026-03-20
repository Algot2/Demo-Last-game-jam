using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[Serializable]
public class atack {
    public GameObject HurtBox;
    public float timeToAttack;
    public float attackTime;
    public int AtackAnimasen;
}
public class PlAtackMan : MonoBehaviour
{
    [SerializeField]
    public List<atack> atacks = new List<atack>();
    public HellfSlider HellfSlider;
    public PlMoment PlMoment;
    public Animator Animato;
    public void PreformAtack(int i) {

        StartCoroutine(Timer.RunAfterTimer(atacks[i].timeToAttack, 
                () => StartCoroutine(Timer.StartTimer(atacks[i].attackTime, (f) => atacks[i].HurtBox.SetActive(f)))));

           
        StartCoroutine(Timer.RunAfterTimer(0.5f, 
                () => StartCoroutine(Timer.StartTimer(1f, (f) => HellfSlider.imune += f ? 1 : -1))));

        if (atacks[i].AtackAnimasen == 0) { Animato.SetTrigger("AtckS"); StartCoroutine(AfterAni()); }
        else Animato.SetTrigger("AtckF");


        //float sped = PlMoment.Sped;
        //PlMoment.Sped = 0; 
        // StartCoroutine(Timer.RunAfterTimer(atacks[i].time, () => PlMoment.Sped = sped));
    }

    IEnumerator AfterAni() {
        while(Animato.GetCurrentAnimatorStateInfo(0).IsName("AtckSlow")) {
            yield return null;
            if (Input.GetMouseButtonUp(0)) {
                atacks[0].HurtBox.SetActive(false);
                PreformAtack(1);
            }

        }
    }
}