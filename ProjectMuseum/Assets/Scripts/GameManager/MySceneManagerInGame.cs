using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneManagerInGame : MonoBehaviour
{
    #region RoomGeneration

    [field: SerializeField] public RoomInstantiator[] RoomInstanciators { get; set; } = new RoomInstantiator[3];
    [field: SerializeField] public List<GameObject> RoomsL { get; set; }
    [field: SerializeField] public List<GameObject> RoomsM { get; set; }
    [field: SerializeField] public List<GameObject> RoomsR { get; set; }
    public int Floor { get; set; } = 0;

    #endregion



    private void Awake()
    {
        if (GameManager.Instance) GameManager.Instance.SMinGame = this;
    }

}
