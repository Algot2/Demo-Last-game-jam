using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleprtToBoss : MonoBehaviour
{
    AsyncOperation async;
    public void PrelodeBoss() {
        async = SceneManager.LoadSceneAsync("Boss", LoadSceneMode.Single);
        async.allowSceneActivation = false;
    }

    public void TeleportToBoss() {
        SceneManager.UnloadSceneAsync(1);
        async.allowSceneActivation = true;
    }
    void Update()
    {
        
    }
}
