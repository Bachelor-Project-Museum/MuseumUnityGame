using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{




    void Start()
    {
        if (GameManager.Instance)
        {
            Debug.Log(GameManager.Instance.SMinGame.RoomInstanciators.Length);

            for (int i = 0; i < GameManager.Instance.SMinGame.RoomInstanciators.Length; i++)
            {
                GameManager.Instance.SMinGame.RoomInstanciators[i].InstantiateRoom();
            }
        }
    }

}
