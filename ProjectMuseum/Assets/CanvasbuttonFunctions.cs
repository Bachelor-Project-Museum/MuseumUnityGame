using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasbuttonFunctions : MonoBehaviour
{
    [SerializeField] GameObject settingsCanva = null;
    AsyncOperation operation;
    readonly string sceneName = "Museum";

    public void OnStartGame()
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnOpenSettings()
    {
        settingsCanva.SetActive(true);
    }

    public void OnOpenCredits()
    {

    }
}
