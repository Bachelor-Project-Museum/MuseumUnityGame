using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject camHolder;
    public float speed = 4;
    public float sprintSpeedMultiplier = 7;
    public float sensitivity = 0.1f;
    public float maxForce = 1;
    public float jumpForce = 3;

    private Vector2 move, look;
    private float lookRotation = 0;

    public bool isGrounded = false;
    private bool isSprinting = false;
    public bool IsSprinting
    {
        get { return isSprinting; }
        set
        {
            isSprinting = value;
            Debug.Log(value);
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        Look();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnSprintStart(InputAction.CallbackContext context)
    {
        IsSprinting = true;
    }

    public void OnSprintRelease(InputAction.CallbackContext context)
    {
        IsSprinting = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Jump()
    {
        Vector3 jumpForces = Vector3.zero;
        if (isGrounded)
        {
            jumpForces = Vector3.up * jumpForce;
        }

        rb.AddForce(jumpForces, ForceMode.VelocityChange);
    }

    private void Move()
    {
        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);

        if (IsSprinting)
        {
            targetVelocity *= (speed * sprintSpeedMultiplier);
        }
        else
        {
            targetVelocity *= speed;
        }

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = targetVelocity - currentVelocity;
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    private void Look()
    {
        // Left-right rotation
        transform.Rotate(Vector3.up * look.x * sensitivity);

        // Up-down rotation
        lookRotation += (-look.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -45, 45);
        camHolder.transform.eulerAngles = new Vector3(lookRotation, camHolder.transform.eulerAngles.y, camHolder.transform.eulerAngles.z);
    }

    public void SetGrounded(bool state)
    {
        isGrounded = state;
    }
}
