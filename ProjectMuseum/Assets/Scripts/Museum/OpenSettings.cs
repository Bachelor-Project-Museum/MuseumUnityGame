using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpenSettings : MonoBehaviour
{
    private CharacterController _characterController;
    private Rigidbody _playerRb;
    private bool gamePaused = false;

    private InputController _inputController;
    private InputAction _menu;

    [SerializeField]
    private GameObject _menuPanel;


    private void Start()
    {
        _inputController = new InputController();
        _characterController = GetComponent<CharacterController>();
        _playerRb = GetComponent<Rigidbody>();
        _menu = _inputController.Player.Menu;
        _menu.Enable();
        _menu.performed += OpenMenuAction;

        _menuPanel.gameObject.SetActive(false);
        gamePaused = false;
    }
    public void OpenMenuAction(InputAction.CallbackContext context)
    {
        DoAction();
    }
    private void DoAction()
    {
        if (!gamePaused)
        {
            OpenMenu();
        }
        else if (gamePaused)
        {
            CloseMenu();
        }
    }
    private void OpenMenu()
    {
        gamePaused = true;
        _menuPanel.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _characterController.enabled = false;
        _playerRb.isKinematic = true;
    }
    public void CloseMenu()
    {
        gamePaused = false;
        _menuPanel.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _characterController.enabled = true;
        _playerRb.isKinematic = false;
    }
}
