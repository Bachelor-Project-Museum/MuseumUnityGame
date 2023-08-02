using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float mouseSensitivy = 0.2f;

    [Header("Optional Settings")]
    [SerializeField] private float airAccelartion = 2f;
    [SerializeField] private float groundAcceleration = 8f;
    [SerializeField] private int maxCameraClamp = 30;
    [SerializeField] private int minCameraClamp = -60;
    [SerializeField] private int cameraLerp = 40;

    private float currentAcceleration;
    private float movementSpeed;
    private bool isRunning;
    private bool GamePaused;
    private Rigidbody rb;
    private Transform cameraTransform;
    private Vector3 lerpedInput;
    private Vector3 currentVelocity;
    private Vector3 camAngle;
    private Vector3 camLerpAngle;


    #region Controls
    private InputController _inputController;
    private InputAction _jump;
    private InputAction _move;
    private InputAction _look;
    private InputAction _run;
    #endregion

    void Awake()
    {
        _inputController = new InputController();

        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentAcceleration = groundAcceleration;

        isRunning = false;
    }

    private void OnEnable()
    {
        _jump = _inputController.Player.Jump;
        _jump.Enable();
        _jump.performed += Jump;

        _move = _inputController.Player.Movement;
        _move.Enable();

        _run = _inputController.Player.Run;
        _run.Enable();
        _run.performed += Run;
        _run.canceled += Run;

        _look = _inputController.Player.Look;
        _look.Enable();
    }

    private void Update()
    {
        if (GamePaused)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
            GetPlayerRotation();
            Movement();
            cameraTransform.eulerAngles = camLerpAngle;
        }
    }

    private void FixedUpdate()
    {
        if (!GamePaused) FixedMovement();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        bool isGrounded = IsGrounded();

        if (context.performed && isGrounded)
        {
            currentVelocity.y = jumpHeight;
            rb.velocity = currentVelocity;
        }
    }

    private void Movement()
    {
        bool isGrounded = IsGrounded();
        Vector3 input = Vector3.zero;

        //InputAbfrage
        input.x = _move.ReadValue<Vector2>().x;
        input.z = _move.ReadValue<Vector2>().y;
        input = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * input;

        //Beschleunigung abhängig ob Ground oder Air
        currentAcceleration = isGrounded ? groundAcceleration : airAccelartion;
        lerpedInput = Vector3.Lerp(lerpedInput, input, currentAcceleration * Time.fixedDeltaTime);

        //Geschwindigkeit abhängig ob isRunning oder isCrouching
        
        movementSpeed = isRunning ? runSpeed : walkSpeed;

        currentVelocity = lerpedInput * movementSpeed;
        currentVelocity.y = rb.velocity.y;
    }

    private void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = true;
        }
        else if (context.canceled) isRunning = false;
    }

    public void GetPlayerRotation()
    {
        camAngle.x -= _look.ReadValue<Vector2>().y * mouseSensitivy;
        camAngle.y += _look.ReadValue<Vector2>().x * mouseSensitivy;

        camAngle.x = Mathf.Clamp(camAngle.x, minCameraClamp, maxCameraClamp);
        camLerpAngle = Vector3.Lerp(camLerpAngle, camAngle, cameraLerp * Time.deltaTime);
    }

    private void FixedMovement()
    {
        //Drehe den Spieler zur Kamera
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        //Passe die Beschleunigung an
        rb.velocity = currentVelocity;
    }

    private bool IsGrounded()
    {
        bool groundCheck = Physics.Raycast(transform.position, Vector3.down, 1.5f);
        return groundCheck;
    }

    private void OnDisable()
    {
        _jump.Disable();
        _jump.performed -= Jump;
        _move.Disable();
        _look.Disable();
        _run.Disable();
        _run.performed -= Run;
        _run.canceled -= Run;
    }


}
