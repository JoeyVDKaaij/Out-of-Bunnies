using UnityEngine;
using JoUnityAddOn.SceneManagement;

public class UIHandlerScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadNextScene();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
