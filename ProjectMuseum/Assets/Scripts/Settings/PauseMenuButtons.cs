using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject _settingsPanel;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip buttonClickSound;

    AsyncOperation operation;
    readonly string sceneName = "MainMenu";

    private void Start()
    {
        _settingsPanel.gameObject.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        _settingsPanel.gameObject.SetActive(true);
        _pauseMenuPanel.gameObject.SetActive(false);

    }

    public void CloseSettingsPanel()
    {
        _settingsPanel.gameObject.SetActive(false);
        _pauseMenuPanel.gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        operation = SceneManager.LoadSceneAsync(sceneName);
    }

    public void PlaySound()
    {
        if(audioSource.clip != buttonClickSound)
        {
            audioSource.clip = buttonClickSound;
        }
        audioSource.Play();
    }
}
