using UnityEngine;

public class MoveObj : MonoBehaviour {
    public Vector3 ofset;
    public float sped;

    bool move = false;
    Vector3 org;
    public void Move() {
        Debug.Log("Obj has moved");
        org = transform.localPosition;
        move = true;
    }


    private void Update()
    {
        if (move) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, ofset + org, Time.deltaTime*50*sped);
        }
    }
}
