using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyControler : MonoBehaviour
{
    public EnemyMovment movment;
    public Transform player;
    public HellfSlider hellf;
    public Rigidbody rb;
    public GameObject[] Atacks;
    public Animator anim;

    public float Atackdureashen = 0.5f;
    public bool ded = false;

   
    void Update() {
       
            if (player.IsUnityNull())
                player = GameManager.Instance.PL;

            if (hellf.curnt <= 0 && !ded)
            {
                ded = true;
                movment.agent.enabled = false;
                movment.enabled = false;
                rb.freezeRotation = false;
            }

            if (!ded)
            {
                if (Vector3.Distance(rb.transform.position, player.position) > 10 || Atackdureashen < 0)
                    movment.agent.speed = 5;

                if (Vector3.Distance(rb.transform.position, player.position) < 10 && Atackdureashen > 0) {
                    movment.agent.speed = 0;

                    if (Vector3.Distance(rb.transform.position, player.position) < 6) {
                        rb.AddForce((rb.transform.position - player.position) * Time.deltaTime * 100);
                    }
                }


                if (Vector3.Distance(rb.transform.position, player.position) < 10)
                {
                    Atackdureashen -= Time.deltaTime;
                    rb.transform.LookAt(player);
                }
                else Atackdureashen = Mathf.Max(Atackdureashen, 0.5f);

                if (Atackdureashen < 0 && Vector3.Distance(rb.transform.position, player.position) < 1.5f && !ded)
                {
                    anim.SetTrigger("Atack");
                    atack(Random.Range(0, Atacks.Length), 1f);
                    Atackdureashen = Random.Range(1f, 2f);

                }
                movment.target = player.position;
            }
    }
    
    public void atack(int atack, float atackLegf) {
       StartCoroutine(Timer.RunAfterTimer(0.5f,() => StartCoroutine(Timer.StartTimer(atackLegf, (f) => Atacks[atack].SetActive(f)))));
    }
}
