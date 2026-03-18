using UnityEngine;

public class Bilbord : MonoBehaviour
{
   
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
