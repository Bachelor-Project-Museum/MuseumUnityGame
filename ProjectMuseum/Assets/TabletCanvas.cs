using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TabletCanvas : MonoBehaviour
{
    [SerializeField] ElevatorDoors[] doors = new ElevatorDoors[2];
    [SerializeField] float elevatorTime = 4;
    readonly int one = 1;
    Coroutine coroutine = null;

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
            //Debug.Log("up");
            OnButtonUp();
        }
        else OnButtonDown();
        //Debug.Log($"Floor: {GameManager.Instance.SMinGame.Floor}");
    }

    // Function to move up a floor
    void OnButtonUp()
    {
        if (GameManager.Instance)
        {
            if (GameManager.Instance.SMinGame)
            {
                coroutine = StartCoroutine(ControlDoors());
                if (GameManager.Instance.SMinGame.RoomsL.Count - one > GameManager.Instance.SMinGame.Floor &&
                    GameManager.Instance.SMinGame.RoomsM.Count - one > GameManager.Instance.SMinGame.Floor &&
                    GameManager.Instance.SMinGame.RoomsR.Count - one > GameManager.Instance.SMinGame.Floor)
                {
                    GameManager.Instance.SMinGame.Floor += 1;
                    foreach (var instatiaor in GameManager.Instance.SMinGame.RoomInstanciators)
                    {
                        instatiaor.DestroyRoom();
                        instatiaor.InstantiateRoom();
                    }
                }
            }
        }
    }

    // Function to move down a floor
    void OnButtonDown()
    {
        if (GameManager.Instance)
        {
            if (GameManager.Instance.SMinGame)
            {
                coroutine = StartCoroutine(ControlDoors());
                if (GameManager.Instance.SMinGame.Floor > 0)
                {
                    GameManager.Instance.SMinGame.Floor -= 1;
                    foreach (var instatiaor in GameManager.Instance.SMinGame.RoomInstanciators)
                    {
                        instatiaor.DestroyRoom();
                        instatiaor.InstantiateRoom();
                    }
                }
            }
        }
    }

    // Enumerator to control the doors behavior
    IEnumerator ControlDoors()
    {
        float time = 0;

        // Closing doors
        foreach (var door in doors)
        {
            door.StartCloseDoor();
        }

        // Cooldown until they can open again
        while (time < elevatorTime)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Opening doors
        foreach (var door in doors)
        {
            door.StartOpenDoor();
        }
        StopCoroutine(coroutine);
    }
}
