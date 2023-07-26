using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] GameObject interactionText;
    [SerializeField] GameObject assetInfoPanel;

    private bool _infoIsActive = false;


    void Start()
    {
        interactionText.SetActive(false);
        assetInfoPanel.SetActive(false);
        _infoIsActive = false;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4f))
        {
            //Wenn Raycast Asset trifft
            if (hit.collider.CompareTag("Interactable"))
            {
                //Abfrage ob InfoPanel angezeigt wird
                if (_infoIsActive)
                {
                    interactionText.SetActive(false);
                }
                else
                {
                    interactionText.SetActive(true);
                }
                
                //Abfrage ob Interaction Button gedrückt wird
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactionText.SetActive(false);
                    
                    if (_infoIsActive == false)
                    {
                        _infoIsActive = true;
                        hit.collider.GetComponent<AssetController>().SetText();
                        interactionText.SetActive(false);
                        assetInfoPanel.SetActive(true);
                    }
                    else if (_infoIsActive == true)
                    {
                        _infoIsActive = false;
                        assetInfoPanel.SetActive(false);
                        interactionText.SetActive(true);
                    }
                }
            }
            else
            {
                _infoIsActive = false;
                interactionText.SetActive(false);
                assetInfoPanel.SetActive(false);
            }
        }
        else
        {
            _infoIsActive = false;
            interactionText.SetActive(false);
            assetInfoPanel.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 4f);
    }
}
