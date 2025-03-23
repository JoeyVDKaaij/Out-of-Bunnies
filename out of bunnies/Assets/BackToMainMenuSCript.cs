using System;
using UnityEngine;
using JoUnityAddOn.SceneManagement;

public class BackToMainMenuSCript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
