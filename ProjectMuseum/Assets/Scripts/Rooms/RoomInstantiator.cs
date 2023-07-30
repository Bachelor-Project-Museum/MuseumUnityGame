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
                    Instantiate(GameManager.Instance.SMinGame.RoomsL[GameManager.Instance.SMinGame.Floor], gameObject.transform);
                break;

            case RoomPosIndex.RoomsM:
                if (GameManager.Instance.SMinGame.Floor <= GameManager.Instance.SMinGame.RoomsM.Count)
                    Instantiate(GameManager.Instance.SMinGame.RoomsM[GameManager.Instance.SMinGame.Floor], gameObject.transform);
                break;

            case RoomPosIndex.RoomsR:
                if (GameManager.Instance.SMinGame.Floor <= GameManager.Instance.SMinGame.RoomsR.Count)
                    Instantiate(GameManager.Instance.SMinGame.RoomsR[GameManager.Instance.SMinGame.Floor], gameObject.transform);
                break;

            default:
                break;
        }
    }

    public void DestroyRoom()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}
