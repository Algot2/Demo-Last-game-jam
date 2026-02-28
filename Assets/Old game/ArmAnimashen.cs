using UnityEngine;

public class ArmAnimashen : MonoBehaviour
{
    public Transform armJont;
    public Transform[] Rotashen;

    public int state;
    void Update() {
        armJont.rotation = Quaternion.Lerp(armJont.rotation, Rotashen[state].rotation, Time.deltaTime * 15);
    }
}
