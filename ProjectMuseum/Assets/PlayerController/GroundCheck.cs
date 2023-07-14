using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerController.gameObject) return;

        playerController.SetGrounded(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject) return;

        playerController.SetGrounded(false);
    }

    private void OnTriggeStay(Collider other)
    {
        if (other.gameObject == playerController.gameObject) return;

        playerController.SetGrounded(true);
    }
}
