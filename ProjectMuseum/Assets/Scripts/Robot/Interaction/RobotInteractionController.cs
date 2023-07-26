using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotInteractionController : MonoBehaviour
{
    [SerializeField] GameObject followPlayerText;
    [SerializeField] GameObject teleportPlayerText;

    [SerializeField, Range(2, 10)] private float maxDistance = 4f;

    private bool _infoIsActive = false;
    private RobotAI _currentRobotAI;

    // Start is called before the first frame update
    void Start()
    {
        followPlayerText.SetActive(false);
        teleportPlayerText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Get the camera's transform
        Transform cameraTransform = Camera.main.transform;

        // Create a Raycast from the camera position, going forward (the direction the camera is facing)
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        // Create a RaycastHit variable to store the hit information
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            // Check if the hit object has the "Robot" tag
            if (hit.collider.CompareTag("Robot"))
            {
                // The player is looking at a robot within the maximum distance
                // Now we can enable the interaction UI for the robot.
                EnableInteractionUI(true);

                _currentRobotAI = hit.collider.GetComponent<RobotAI>();

                // ToggleFollow player 
                if (Input.GetKeyDown(KeyCode.F) && _currentRobotAI != null)
                {
                    _currentRobotAI.ToggleFollowPlayer();
                } 

                // Teleport
                if (Input.GetKeyDown(KeyCode.T))
                {
                    _currentRobotAI.TeleportPlayer();
                }
            }
            else
            {
                // If the player is not looking at a robot, disable the interaction UI.
                EnableInteractionUI(false);
            }
        }
        else
        {
            // If the Raycast doesn't hit anything, disable the interaction UI.
            EnableInteractionUI(false);
        }
    }

    private void EnableInteractionUI(bool enable)
    {
        _infoIsActive = enable;
        followPlayerText.SetActive(enable);
        teleportPlayerText.SetActive(enable);
    }
}
