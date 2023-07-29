using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TabletCanvas : MonoBehaviour
{
    private enum Direction
    {
        Up,
        Down
    }

    [SerializeField] Direction direction = Direction.Down;

    public void ActivateElevator()
    {
        if (direction == Direction.Up)
        {
            Debug.Log("up");
            OnButtonUp();
        }
        else OnButtonDown();
        Debug.Log($"Floor: {GameManager.Instance.SMinGame.Floor}");
    }

    void OnButtonUp()
    {
        if (GameManager.Instance)
        {
            if (GameManager.Instance.SMinGame)
            {
                GameManager.Instance.SMinGame.Floor++;
                foreach (var instatiaor in GameManager.Instance.SMinGame.RoomInstanciators)
                {
                    instatiaor.DestroyRoom();
                    instatiaor.InstantiateRoom();
                }
            }
        }
    }

    void OnButtonDown()
    {
        if (GameManager.Instance)
        {
            if (GameManager.Instance.SMinGame)
            {
                if (GameManager.Instance.SMinGame.Floor > 0) GameManager.Instance.SMinGame.Floor--;
                foreach (var instatiaor in GameManager.Instance.SMinGame.RoomInstanciators)
                {
                    instatiaor.DestroyRoom();
                    instatiaor.InstantiateRoom();
                }
            }
        }
    }
}
