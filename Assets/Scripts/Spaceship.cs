using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Spaceship : MonoBehaviour
{
    [Header("=== Ship Movement Settings ===")]
    [SerializeField]
    private float yawTorque = 500f;
    [SerializeField]
    private float pitchTorque = 500f;
    [SerializeField]
    private float rollTorque = 1000f;
    [SerializeField]
    private float forwardThrust = 1000f;
    [SerializeField]
    private float verticalThrust = 750f;
    [SerializeField]
    private float horizontalThrust = 750f;

    Rigidbody rb;
    GameObject controlsWindow;

    //Input values
    private float forward1D;
    private float horizontal1D;
    private float vertical1D;
    private float roll1D;
    private Vector2 pitchYaw;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controlsWindow = GameObject.FindGameObjectWithTag("Binds");
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Forward
        if (Mathf.Abs(forward1D) > 0.1f)
        {
            rb.AddRelativeForce(Vector3.forward
                * forward1D
                * forwardThrust
                * Time.deltaTime,
                ForceMode.Impulse
                );
        }

        // Horizontal
        if (Mathf.Abs(horizontal1D) > 0.1f)
        {
            rb.AddRelativeForce(Vector3.right
                * horizontal1D
                * horizontalThrust
                * Time.deltaTime,
                ForceMode.Impulse
                );
        }

        // Vertical
        if (Mathf.Abs(vertical1D) > 0.1f)
        {
            rb.AddRelativeForce(Vector3.up
                * vertical1D
                * verticalThrust
                * Time.deltaTime,
                ForceMode.Impulse
                );
        }

        // Roll
        if (Mathf.Abs(roll1D) > 0.1f)
        {
            rb.AddRelativeTorque(Vector3.back
                * roll1D
                * rollTorque
                * Time.deltaTime,
                ForceMode.Impulse
                );
        }

        // Pitch
        if (Mathf.Abs(pitchYaw.x) > 0.1f)
        {
            rb.AddRelativeTorque(Vector3.up
                * Mathf.Clamp(pitchYaw.x, -1f, 1f)
                * pitchTorque
                * Time.deltaTime
                );
        }

        // Yaw
        if (Mathf.Abs(pitchYaw.y) > 0.1f)
        {
            rb.AddRelativeTorque(Vector3.right
                * Mathf.Clamp(-pitchYaw.y, -1f, 1f)
                * yawTorque
                * Time.deltaTime
                );
        }        
    }

    #region Input Methods
    public void OnForward(InputAction.CallbackContext context)
    {
        forward1D = context.ReadValue<float>();
    }

    public void OnHorizontal(InputAction.CallbackContext context)
    {
        horizontal1D = context.ReadValue<float>();
    }

    public void OnVertical(InputAction.CallbackContext context)
    {
        vertical1D = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }

    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw = context.ReadValue<Vector2>();
    }

    public void OnControlsWindow(InputAction.CallbackContext context)
    {
        bool state = !controlsWindow.activeSelf;
        controlsWindow.SetActive(state);
    }
    #endregion
}
