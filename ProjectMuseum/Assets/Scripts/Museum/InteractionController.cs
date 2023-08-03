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

    [SerializeField, Range(2, 10)] private float maxDistance = 4f;

    private RobotAI _currentRobotAI;
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
        //Interactables
        interactionText.SetActive(false);
        assetInfoPanel.SetActive(false);
        _infoIsActive = false;

        //Robot
        followPlayerText.SetActive(false);
        teleportPlayerText.SetActive(false);
    }



    void Update()
    {
        willDisableUIatEndOfFrame = false;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4f))
        {
            //On Raycasthit
            CheckForInfoWindow();

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
        //Checks if window is active
        if (_infoIsActive) interactionText.SetActive(false);
        else interactionText.SetActive(true);
        
    }

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

    private void OnRaycasthitRobot(RaycastHit rayHit)
    {
        // The player is looking at a robot within the maximum distance
        // Now we can enable the interaction UI for the robot.
        EnableInteractionUI(true);

        _currentRobotAI = rayHit.collider.GetComponent<RobotAI>();

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

    private void OnRaycasthitButton(RaycastHit rayHit)
    {
        Debug.Log("Ray Button");
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Got Button OnClick");
            rayHit.transform.gameObject.GetComponent<TabletCanvas>().ActivateElevator();
        }
    }
}
