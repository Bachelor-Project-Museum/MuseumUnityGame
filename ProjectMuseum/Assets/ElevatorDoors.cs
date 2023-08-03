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

    void Update()
    {
        
    }

    public void StartOpenDoor()
    {
        coroutine = StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        float timer = 0;

        while(Vector3.Distance(gameObject.transform.position, openPos.position) > 0.1f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, openPos.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        StopCoroutine(coroutine);
    }

    public void StartCloseDoor()
    {
        coroutine = StartCoroutine(CloseDoor());
    }

    IEnumerator CloseDoor()
    {
        float timer = 0;

        while (Vector3.Distance(gameObject.transform.position, closePos.position) > 0.01f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, closePos.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        StopCoroutine(coroutine);
    }
}