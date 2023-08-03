using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenSettings : MonoBehaviour
{
    private CharacterController characterController;
    private bool gamePaused = false;

    private InputController inputController;
    private InputAction menu;

    private GameObject _menuPanel;

    private void Start()
    {
        inputController = new InputController();
        menu = inputController.Player.Menu;
        menu.Enable();
        menu.performed += OpenMenu;

        _menuPanel = GameObject.FindGameObjectWithTag("Menu");
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        Debug.Log("open Menu");
    }
}
