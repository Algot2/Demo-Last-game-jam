using UnityEngine;

public class CamraMoment : MonoBehaviour
{
    public Vector3 direct;
    public Transform head;
    public float disToCam;
    public Transform cam;
    public float some = 0;
    Vector2 mosePos;
    public void setCamraDireksen(Vector2 mosePosDelta, float s) {
        some += s * Time.deltaTime*10;
        mosePos += mosePosDelta * Time.deltaTime;
        mosePos = new Vector2(mosePos.x, Mathf.Clamp(mosePos.y, -0.55f, -0.25f));
        some = Mathf.Clamp(some, 0, 1);
        direct = SphericalToCartesian(disToCam - some, mosePos.x * Mathf.PI, mosePos.y * Mathf.PI);
        cam.transform.position = head.position + new Vector3(direct.x, direct.y, direct.z);
        cam.transform.LookAt(head);
        cam.transform.position -= head.right * 1.2f;
        head.forward = new Vector3(direct.x, 0, direct.z).normalized;

    }

    public Vector3 SphericalToCartesian(float radius, float theta, float phi) {
        float x = Mathf.Sin(phi) * Mathf.Cos(theta);
        float y = Mathf.Cos(phi);
        float z = Mathf.Sin(phi) * Mathf.Sin(theta);
        Vector3 dir = new Vector3(x, y, z); 
        
        if (Physics.Raycast(head.transform.position, dir, out var hit, radius, GameManager.Instance.ground)) {
            radius = Mathf.Clamp(Vector3.Distance(head.transform.position, hit.point) - 0.5f, 1, radius);
        }
        return new Vector3(x, y, z) * radius;
    }
}
