using UnityEngine;
using UnityEngine.UIElements;

public class EnemyControler : MonoBehaviour
{
    public EnemyMovment movment;
    public Transform player;
    public HellfSlider hellf;
    public Rigidbody rb;
    public GameObject[] Atacks;
    public GameObject sper1, sper2;
    public Animator anim;

    public float Atackdureashen = 0.5f;
    bool ded = false;
    void Update() {
        if (hellf.curnt <= 0 && !ded)
        {
            ded = true;
            movment.agent.enabled = false;
            movment.enabled = false;
            rb.freezeRotation = false;
        }
        
        if (Vector3.Distance(rb.transform.position, player.position) > 4 || Atackdureashen < 0)
            movment.agent.speed = 5;
        
        if (Vector3.Distance(rb.transform.position, player.position) < 4 && Atackdureashen > 0)
            movment.agent.speed = 0;


        if (Vector3.Distance(rb.transform.position, player.position) < 5) Atackdureashen -= Time.deltaTime;
        else Atackdureashen = Mathf.Max(Atackdureashen, 1f);

        if (Atackdureashen < 0 && Vector3.Distance(rb.transform.position, player.position) < 1.5f && !ded)
        {
            anim.SetTrigger("Atack");
            atack(Random.Range(0, Atacks.Length), 1f);
            Atackdureashen = 3;

        }


        movment.target = player.position;
         
        sper1.SetActive(Atackdureashen < 0);
        sper2.SetActive(Atackdureashen < 0);


        anim.SetBool("Run", movment.agent.speed != 0);
       
    }

   

    public void atack(int atack, float atackLegf) {
       StartCoroutine(Timer.RunAfterTimer(0.5f,() => StartCoroutine(Timer.StartTimer(atackLegf, (f) => Atacks[atack].SetActive(f)))));
    }

   
}
