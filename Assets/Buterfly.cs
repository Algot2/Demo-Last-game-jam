using UnityEngine;

public class Buterfly : MonoBehaviour
{
    Vector3 Rotpont = Vector3.zero;
    public float prefurdDis = 5;
    public float seed = 10;
    public int dir = -1;
    Vector3 pikeNewPos() {
        Vector3 pl = GameManager.player.position;

        return pl + new Vector3 {
            x = Random.Range(-1f, 1f),
            y = Random.Range(0.1f, 0.5f),
            z = Random.Range(-1f, 1f)
        }.normalized * Random.Range(0, 10);
    }
    private void Start()
    {
        transform.localScale *= Random.Range(0.9f, 1.2f);
        dir = Random.Range(0, 2) == 0 ? -1 : 1;
        seed = Random.Range(5, 10);
        prefurdDis = Random.Range(5f, 10f);

        transform.position = pikeNewPos();
    }
    void Update() {

        Vector3 dis = Rotpont - transform.position;
        if (Vector3.Distance(Rotpont, GameManager.player.transform.position) > 10) {
            Rotpont = pikeNewPos();
        }
        Vector3 move = Vector3.Cross(new Vector3(dis.x, dis.y*dir, dis.z).normalized, transform.up) * dir;
        if (Mathf.Abs(dis.magnitude - prefurdDis) > 1) 
            move += dis.normalized * (dis.magnitude < prefurdDis ? -1 : 1);
        transform.forward = Vector3.Lerp(transform.forward, move, Time.deltaTime * 10);
        transform.position += move * Time.deltaTime * seed;

        if (Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down, out var hit, 100, GameManager.Instance.ground)
           && hit.point.y - transform.position.y > 0)
        {
            transform.position += Vector3.up * Time.deltaTime * 10;

        }
    }
}
