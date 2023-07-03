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
                room = GameManager.Instance.SMinGame.RoomsL[GameManager.Instance.SMinGame.Floor];
                break;

            case RoomPosIndex.RoomsM:
                room = GameManager.Instance.SMinGame.RoomsM[GameManager.Instance.SMinGame.Floor];
                break;

            case RoomPosIndex.RoomsR:
                room = GameManager.Instance.SMinGame.RoomsR[GameManager.Instance.SMinGame.Floor];
                break;

            default:
                break;
        }

        Instantiate(room, gameObject.transform);
    }

    public bool DestroyRoom()
    {
        Destroy(room);
        if (gameObject.transform.childCount == 0) return true;
        else return false;
    }
}
