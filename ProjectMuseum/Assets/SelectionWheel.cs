using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionWheel : MonoBehaviour
{

    Coroutine rotationCoroutine = null;
    [SerializeField] float rotationSpeed = 5f; 
    bool isRotating = false;
    int oneRotationRadiusClockwise = 90;
    int oneRotationRadiusCounterclockwise = -90;
    readonly int one = 1;
    float rotationZ = 0f;

    [SerializeField] RectTransform ownRectTrans = null;


    private void Start()
    {
        ownRectTrans.rotation = Quaternion.Euler(ownRectTrans.rotation.x, ownRectTrans.rotation.y, rotationZ);
    }
    public void StartRotation(bool willRotateClockwise)
    {
        if (!isRotating) rotationCoroutine = StartCoroutine(RotationCoroutine(willRotateClockwise));
    }
    
    
    IEnumerator RotationCoroutine(bool willRotateClockwise)
    {
        // Calculate the target and start rotation
        float newRotationZ = rotationZ + (willRotateClockwise ? oneRotationRadiusClockwise : oneRotationRadiusCounterclockwise);

        // Define the start and end rotation angles as Quaternions
        Quaternion startRot = Quaternion.Euler(ownRectTrans.rotation.x, ownRectTrans.rotation.y, rotationZ);
        Quaternion endRot = Quaternion.Euler(ownRectTrans.rotation.x, ownRectTrans.rotation.y, newRotationZ);

        float elapsedTime = 0f;

        isRotating = true;
        while (true)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;

            // Rotation is complete
            if (elapsedTime > one)
            {
                isRotating = false;
                //Debug.Log("EndRot:" + ownRectTrans.rotation.z);
                rotationZ = newRotationZ;
                ownRectTrans.rotation = Quaternion.Euler(ownRectTrans.rotation.x, ownRectTrans.rotation.y, newRotationZ);
                StopCoroutine(rotationCoroutine);
            }

            // Perform a rotaion based on slerp
            ownRectTrans.rotation = Quaternion.Slerp(startRot, endRot, elapsedTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
