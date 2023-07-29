using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstantiator : MonoBehaviour
{
    [SerializeField] RoomPosIndex roomPos;

    GameObject room = null;

    public enum RoomPosIndex
    {
        RoomsL,
        RoomsM,
        RoomsR
    }



    public void InstantiateRoom()
    {
        Debug.Log(roomPos);


        switch (roomPos)
        {
            case RoomPosIndex.RoomsL:
                if (GameManager.Instance.SMinGame.Floor <= GameManager.Instance.SMinGame.RoomsL.Count)
                    room = GameManager.Instance.SMinGame.RoomsL[GameManager.Instance.SMinGame.Floor];
                break;

            case RoomPosIndex.RoomsM:
                if (GameManager.Instance.SMinGame.Floor <= GameManager.Instance.SMinGame.RoomsM.Count)
                    room = GameManager.Instance.SMinGame.RoomsM[GameManager.Instance.SMinGame.Floor];
                break;

            case RoomPosIndex.RoomsR:
                if (GameManager.Instance.SMinGame.Floor <= GameManager.Instance.SMinGame.RoomsR.Count)
                    room = GameManager.Instance.SMinGame.RoomsR[GameManager.Instance.SMinGame.Floor];
                break;

            default:
                break;
        }

        Instantiate(room, gameObject.transform);
    }

    public void DestroyRoom()
    {
        Destroy(room);
    }
}
