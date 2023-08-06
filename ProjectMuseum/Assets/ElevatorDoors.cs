using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoors : MonoBehaviour
{
    [SerializeField] Transform openPos;
    [SerializeField] Transform closePos;
    [SerializeField] float speed = 0;
    Coroutine coroutine = null;

    void Start()
    {
        gameObject.transform.position = openPos.position;
    }

    public void StartOpenDoor()
    {
        coroutine = StartCoroutine(OpenDoor());
    }

    // Enumerator to open elevator doors
    IEnumerator OpenDoor()
    {
        while(Vector3.Distance(gameObject.transform.position, openPos.position) > 0.1f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, openPos.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        GameManager.Instance.ElevatorDoorMoving = false;

        if (coroutine != null) StopCoroutine(coroutine);
    }

    public void StartCloseDoor()
    {
        coroutine = StartCoroutine(CloseDoor());
    }

    // Enumerator to close elevator doors
    IEnumerator CloseDoor()
    {
        while (Vector3.Distance(gameObject.transform.position, closePos.position) > 0.01f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, closePos.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        GameManager.Instance.ElevatorDoorMoving = false;

        if (coroutine != null) StopCoroutine(coroutine);
    }
}
