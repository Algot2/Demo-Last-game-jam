using UnityEngine;

public class NewHuerBox : MonoBehaviour
{
    public bool noNokBack;
    public bool IsEnemy;
    public bool SetEmune;
    public float dam;

    public static bool CanDamage = true;

    void OnTriggerEnter(Collider other) 
    {
        
        if(!CanDamage) return;

        if ( SetEmune) {
            StartCoroutine(Timer.StartTimer(0.2f, (f)=>GameManager.Instance.PL.GetComponentInChildren<HellfSlider>().Inmune = f));
        }
        NewHitBox hitBox = other.transform.root.GetComponentInChildren<NewHitBox>();
        if (hitBox == null || hitBox.IsEnemy == IsEnemy) return;

        Vector3 back = (other.transform.position-transform.position).normalized * (noNokBack ? 0 : 1) * 50;
        hitBox.onHit(dam, back);

        if (!IsEnemy && !noNokBack) { 
            StartCoroutine(Efects.camShake(0.1f, 0.2f)); 
            StartCoroutine(Efects.timeFrez(0.3f));
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Time.timeScale = 1.0f;
    }
}
