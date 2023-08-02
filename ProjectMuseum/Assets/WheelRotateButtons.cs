using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotateButtons : MonoBehaviour
{
    private enum RotationDirection
    {
        Right,
        Left
    }
    [SerializeField] RotationDirection direction;
    [SerializeField] SelectionWheel wheelScript;

    public void OnClickEventRotation()
    {
        if (direction == RotationDirection.Left)
        {
            wheelScript.StartRotation(true);
            return;
        }
        wheelScript.StartRotation(false);
    }

}
