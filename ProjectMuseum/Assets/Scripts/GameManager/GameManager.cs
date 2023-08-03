using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public MySceneManagerInGame SMinGame { get; set; }

    [field: SerializeField] public GameObject PlayerObject { get; set; }

    [field: SerializeField] public GameObject RoboterObject { get; set; }

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
