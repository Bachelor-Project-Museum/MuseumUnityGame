using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InteractionController : MonoBehaviour
{
    [SerializeField] GameObject interactionText;
    [SerializeField] GameObject assetInfoPanel;

    private bool _infoIsActive = false;


    #region Robot

    [SerializeField] GameObject followPlayerText;
    [SerializeField] GameObject teleportPlayerText;

    private RobotAI _currentRobotAI;
    private RobotBT _currentRobotBT;
    private bool willDisableUIatEndOfFrame = false;

    #endregion
    private enum RaycastHits
    {
        Interactable,
        Robot,
        Button
    }

    void Start()
    {
        // Initialize UI elements
        interactionText.SetActive(false);
        assetInfoPanel.SetActive(false);
        _infoIsActive = false;

        // Robot
        followPlayerText.SetActive(false);
        teleportPlayerText.SetActive(false);
    }



    void Update()
    {
        willDisableUIatEndOfFrame = false;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Cast a ray to detect interactions with objects
        if (Physics.Raycast(ray, out hit, 4f))
        {
            // On Raycasthit, check for UI elements to be displayed
            CheckForInfoWindow();

            // Determine the type of objects hit and handle accordingly
            RaycastHits tag = default;
            if (System.Enum.TryParse(hit.collider.gameObject.tag, out tag))
            {
                switch (tag)
                {
                    case RaycastHits.Interactable:
                        OnRaycasthitInteractable(hit);
                        break;

                    case RaycastHits.Robot:
                        OnRaycasthitRobot(hit);
                        break;

                    case RaycastHits.Button:
                        OnRaycasthitButton(hit);
                        break;

                    default:
                        break;
                }
            }
            else willDisableUIatEndOfFrame = true;
        }
        else willDisableUIatEndOfFrame = true;

        if (willDisableUIatEndOfFrame)
        {
            DisableDisplayField();
            EnableInteractionUI(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 4f);
    }

    private void CheckForInfoWindow()
    {
        // Checks if window is active
        if (_infoIsActive) interactionText.SetActive(false);
        else interactionText.SetActive(true);
        
    }

    #region UI enable/disable
    private void EnableDisplayField()
    {
        _infoIsActive = true;
        interactionText.SetActive(false);
        assetInfoPanel.SetActive(true);
    }
    private void DisableDisplayField()
    {
        _infoIsActive = false;
        interactionText.SetActive(false);
        assetInfoPanel.SetActive(false);
    }
    private void EnableInteractionUI(bool enable)
    {
        _infoIsActive = enable;
        followPlayerText.SetActive(enable);
        teleportPlayerText.SetActive(enable);
    }

    #endregion

    // Function for raycastHit with interactable object
    private void OnRaycasthitInteractable(RaycastHit rayHit)
    {
                //Abfrage ob Interaction Button gedrückt wird
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionText.SetActive(false);

                    if (_infoIsActive == false)
                    {
                        rayHit.collider.GetComponent<AssetController>().SetText();
                        EnableDisplayField();
                    }
                    else if (_infoIsActive == true) willDisableUIatEndOfFrame = true;
                }
    }

    // Function for raycastHit with robot to interact with
    private void OnRaycasthitRobot(RaycastHit rayHit)
    {
        // The player is looking at a robot within the maximum distance
        // Now we can enable the interaction UI for the robot.
        EnableInteractionUI(true);

        _currentRobotAI = rayHit.collider.GetComponent<RobotAI>();
        _currentRobotBT = rayHit.collider.GetComponent<RobotBT>();


        if (_currentRobotAI != null && _currentRobotAI.enabled)
        {
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
            // ToggleFollow player 
            if (Input.GetKeyDown(KeyCode.F) && _currentRobotAI != null)
            {
                _currentRobotBT.ToggleFollowPlayer();
            }

            // Teleport
            if (Input.GetKeyDown(KeyCode.T))
            {
                _currentRobotBT.TeleportPlayer();
            }
        }
         //= rayHit.collider.GetComponent<RobotBT>();

    }

    // Function for raycastHit with buttons
    private void OnRaycasthitButton(RaycastHit rayHit)
    {
        //Debug.Log("Ray Button");
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("Got Button OnClick");
            rayHit.transform.gameObject.GetComponent<TabletCanvas>().ActivateElevator();
        }
    }
}
