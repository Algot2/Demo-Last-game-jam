using UnityEngine;

public class CamraMoment : MonoBehaviour
{
    public Transform bodyVisols;
    public Vector3 direct;
    public Transform head;
    public float disToCam;
    public Transform cam;
    Vector2 mosePos;
    public void setCamraDireksen(Vector2 mosePosDelta) {
        mosePos += mosePosDelta * Time.deltaTime;
        direct = SphericalToCartesian(disToCam, mosePos.x * Mathf.PI, mosePos.y * Mathf.PI);
        cam.transform.position = head.position + new Vector3(direct.x, direct.y, direct.z);
        cam.transform.LookAt(head);
        cam.transform.position -= head.right * 1.2f;
        head.forward = new Vector3(direct.x, 0, direct.z).normalized;

       // bodyVisols.forward = -head.forward;
    }

    public static Vector3 SphericalToCartesian(float radius, float theta, float phi)
    {
        float x = radius * Mathf.Sin(phi) * Mathf.Cos(theta);
        float y = radius * Mathf.Cos(phi);
        float z = radius * Mathf.Sin(phi) * Mathf.Sin(theta);

        return new Vector3(x, y, z);
    }
}
