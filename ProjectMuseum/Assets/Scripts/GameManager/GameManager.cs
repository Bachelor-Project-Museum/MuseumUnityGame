using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Variables for GameManager

    public MySceneManagerInGame SMinGame { get; set; }




    // _______________________________________________________
    public GameObject PlayerObject { get { return playerObject; } }
    [SerializeField] private GameObject playerObject;

    //[field: SerializeField] public GameObject PlayerOJ { get; set; }

    public GameObject LobbySpawn { get { return lobbySpawn; } }
    [SerializeField] private GameObject lobbySpawn;

    public bool RobotFollow = true;

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
