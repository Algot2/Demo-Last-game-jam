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

    bool isHolding;

    Coroutine attacktimer;

    void Update()
    {
        isHolding = Input.GetMouseButton(0);
    }

    public void PreformAtack(int i) {

        if(i == 0)
            StartCoroutine(AfterAni());
        
        attacktimer = StartCoroutine(Timer.RunAfterTimer(atacks[i].timeToAttack, 
                () => StartCoroutine(Timer.StartTimer(atacks[i].attackTime, (f) => atacks[i].HurtBox.SetActive(f)))));

           
        StartCoroutine(Timer.RunAfterTimer(0.5f, 
                () => StartCoroutine(Timer.StartTimer(1f, (f) => HellfSlider.imune += f ? 1 : -1))));

        if (atacks[i].AtackAnimasen == 0) Animato.SetTrigger("AtckS");
        else Animato.SetTrigger("AtckF");


        //float sped = PlMoment.Sped;
        //PlMoment.Sped = 0; 
        // StartCoroutine(Timer.RunAfterTimer(atacks[i].time, () => PlMoment.Sped = sped));
    }

    IEnumerator AfterAni()
    {
        float timePlayed = 0;
        
        while (!Animato.GetCurrentAnimatorStateInfo(0).IsName("AtckSlow"))
        {
            yield return null;
        }
        
        while(Animato.GetCurrentAnimatorStateInfo(0).IsName("AtckSlow"))
        {
            timePlayed += Time.deltaTime;
            
            yield return null;
            if (!isHolding && timePlayed < 0.25f) 
            {
                atacks[0].HurtBox.SetActive(false);
                Animato.SetBool("Run", false);
                StopCoroutine(attacktimer);
                PreformAtack(1);
                break;
            }
        }
    }
}