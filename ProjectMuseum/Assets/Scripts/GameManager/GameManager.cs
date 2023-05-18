using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables for GameManager
    public GameObject PlayerObject { get { return playerObject; } }
    [SerializeField] private GameObject playerObject;

    public GameObject LobbySpawn { get { return lobbySpawn; } }
    [SerializeField] private GameObject lobbySpawn;


    public bool RobotFollow = true;

    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TeleportToSpawn(int x, int y, int z)
    {
        // Teleport Function
    }

    public void ToggleRobotFollow(bool follow)
    {
        // ToggleRobotFollow Function
        RobotFollow = follow;
    }
    
    // Functions for loading scenes
    //private void GameOver()
    //{
    //    SceneManager.LoadScene("GameOverScene");
    //}
}
