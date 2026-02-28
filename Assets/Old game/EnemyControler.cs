using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public EnemyMovment movment;
    public Transform player;
    public HitBox hitBox;
    public Rigidbody rb;
    public GameObject[] Atacks;
    public GameObject sper1, sper2;

    float Atackdureashen = 0.5f;
    bool ded = false;
    void Update() {
        if (hitBox.hellf <= 0 && !ded)
        {
            ded = true;
            movment.agent.enabled = false;
            rb.freezeRotation = false;
           // rb.AddExplosionForce(50, (transform.position-player.position).normalized, 2, 2);
        }
        
        if (Vector3.Distance(transform.position, player.position) > 4 || Atackdureashen < 0)
            movment.agent.speed = 5;
        
        if (Vector3.Distance(transform.position, player.position) < 4 && Atackdureashen > 0)
            movment.agent.speed = 0;


        if (Vector3.Distance(transform.position, player.position) < 5) Atackdureashen -= Time.deltaTime;
        else Atackdureashen = Mathf.Max(Atackdureashen, 1f);

        if (Atackdureashen < 0 && Vector3.Distance(transform.position, player.position) < 1.5f && !ded) 
            atack(Random.Range(0, Atacks.Length), 2);


        movment.target = player.position;
         
        sper1.SetActive(Atackdureashen < 0);
        sper2.SetActive(Atackdureashen < 0);
    }

   

    public void atack(int atack, float atackLegf)
    {
        if (Atackdureashen < 0)
        {
            Atackdureashen = 2;
            Destroy(Instantiate(Atacks[atack], transform.position, transform.rotation), 0.1f);
        }
    }

   
}
