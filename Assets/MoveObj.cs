using UnityEngine;

public class MoveObj : MonoBehaviour {
    public Vector3 ofset;
    public float sped;

    bool move = false;
    Vector3 org;
    public void Move() {
        Debug.Log("Obj has moved");
        org = transform.position;
        move = true;
    }


    private void Update()
    {
        if (move) {
            transform.position += ofset.normalized * sped * Time.deltaTime;
            move = (org - ofset).magnitude > ofset.magnitude;
        }
    }
}
