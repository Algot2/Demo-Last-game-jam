using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public Animator ani;
    
    [BoltsAnimationClip("ani")]
    public string attack1;
    
    public NewHuerBox damageBox;
    public GameObject hurtBox;

    public Transform defaultHurtBoxParent;

    void Attack(string clip, float damage, float time = 1, Vector3 boxSize = new(), Transform parent = null)
    {
        damageBox.GetComponent<BoxCollider>().size = boxSize;
        damageBox.dam = damage;
        
        hurtBox.SetActive(true);

        if (parent != null)
        {
            hurtBox.transform.parent = parent;
            hurtBox.transform.localPosition = Vector3.zero;
            hurtBox.transform.localRotation = Quaternion.identity;
        }
        
        if(HasClip(clip))
            ani.Play(clip);

        StartCoroutine(DoAnimation(clip, time, ResetDamageBox));
    }
    
    IEnumerator DoAnimation(string clip, float when, Action toDo)
    {
        
        yield return null;

        while (!ani.GetCurrentAnimatorStateInfo(0).IsName(clip))
            yield return null;
        
        while (ani.GetCurrentAnimatorStateInfo(0).normalizedTime < when && ani.GetCurrentAnimatorStateInfo(0).IsName(clip))
        {
            yield return null;
        }
        
        toDo?.Invoke();
    }

    void ResetDamageBox()
    {
        damageBox.GetComponent<BoxCollider>().size = Vector3.one;
        
        hurtBox.transform.parent = defaultHurtBoxParent;
        hurtBox.transform.localPosition = Vector3.zero;
        hurtBox.transform.localRotation = Quaternion.identity;
        
        hurtBox.SetActive(false);
    }

    bool HasClip(string clipName)
    {
        foreach (AnimationClip clip in ani.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName) return true;
        }

        return false;
    }

    [Button]
    void TestAttack()
    {
        Attack(attack1, 100, 1, Vector3.one * 10);
    }
}
