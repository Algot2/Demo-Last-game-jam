using System;
using System.Collections;
using UnityEngine;

public class PlAtacks : MonoBehaviour
{
    public float Atackdureashen;
    public GameObject[] Atacks;
    public HitBox HitBox;
    public ArmAnimashen arm;


    public void atack(int atack, float atackLegf) {
        if (Atackdureashen < 0) {
            StartCoroutine(timer(0.2f, (f) => HitBox.Bloking = f));
            StartCoroutine(timer(0.2f, (f) => arm.state = f ? 1 : 0));
            StartCoroutine(timer(0.2f, (f) => Atacks[atack].SetActive(f)));

            Atackdureashen = atackLegf;
        }
    }

    IEnumerator timer(float t, Action<bool> F) {
        F(true);
        yield return new WaitForSeconds(t);
        F(false);
    }

    private void Update() {
        Atackdureashen -= Time.deltaTime;
    }
}
