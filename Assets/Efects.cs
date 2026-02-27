using System.Collections;
using UnityEngine;

public class Efects : MonoBehaviour
{
    public static IEnumerator timeFrez(float time)
    {
        Debug.Log(time);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
    }

    public static IEnumerator camShake(float duration, float magnitude) {
        Vector3 originalPosition = Camera.main.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = originalPosition + new Vector3(x, y, 0f);

            elapsed += Time.unscaledDeltaTime; // works even if timeScale = 0
            yield return null;
        }

        Camera.main.transform.localPosition = originalPosition;
    }
}
