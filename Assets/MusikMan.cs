using UnityEngine;

public class MusikMan : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource AudioSource;
    public float maxVolum;
    public AnimationCurve VoumeCure;
    public Transform[] checponts;
    public float chenpontInfluens;
    public bool inCombat;

    float GetDistensToClosest() {
        Vector3 dist = Vector3.right * chenpontInfluens;
        
        foreach (Transform t in checponts) {
            Vector3 d = t.position - Camera.main.transform.position;
            if (dist.magnitude > d.magnitude) 
                dist = d;
        }
        return (dist.magnitude)/chenpontInfluens;
    }
    void Update() {
        inCombat = GameManager.Instance.enemies.Count > 0;

        if (!inCombat) {
           AudioSource.volume = VoumeCure.Evaluate(GetDistensToClosest()) * maxVolum;
        }
        else AudioSource.volume = Mathf.Lerp(AudioSource.volume, maxVolum * 2, Time.deltaTime*10);
    }
}
