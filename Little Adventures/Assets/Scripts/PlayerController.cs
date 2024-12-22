using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // inputs
    private Vector2 input;
    private CharacterController characterController;

    // player movement
    private Vector3 direction;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float playerSpeed;

    // gravity
    [SerializeField] private float standardGravity;
    [SerializeField] private float glidingGravity;
    private float currentDownwardVelocity;

    // jumping and gliding
    [SerializeField] private float jumpPower;
    private int jumpCount;
    [SerializeField] private int maxJumps;
    private bool isGliding = false;
    private Transform glidePivot;
    private Quaternion defaultPlayerRotation;
    private float glideTiltAngle = 90f;        // Angle for horizontal tilt
    private float glideStart;

    // camera movement
    private Camera mainCamera;

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        glidePivot = transform.Find("GlidePivot");
        if (glidePivot != null)
        {
            defaultPlayerRotation = glidePivot.localRotation;
        }
    }

    private void Update()
    {
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }

    private void ApplyRotation()
    {
        if (input.sqrMagnitude == 0) return;

        direction = Quaternion.Euler(0.0f, mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(input.x, 0.0f, input.y);
        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (isGliding)
        {
            ApplyTilt();
        }
        else 
        {
            ResetTilt();
        }
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && currentDownwardVelocity < 0.0f)
        {
            currentDownwardVelocity = -1.0f;
        }
        else 
        {
            var currentGravity = standardGravity;
            if (isGliding)
            {
                currentGravity = glidingGravity; // TODO: slowly decrease gravity during glide + Mathf.Log10(Time.time - glideStart);
                // Debug.Log(currentGravity);   
            }
            currentDownwardVelocity += currentGravity * Time.deltaTime;
        }
        direction.y = currentDownwardVelocity;
    }

    private void ApplyMovement()
    {
        characterController.Move(direction * playerSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (characterController == null) {
            characterController = GetComponent<CharacterController>();
        }
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0.0f, input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // perform jump after player presses jump key
        if (context.started)
        {
            // only jump if player is on the ground or already jumped max times
            if (!PlayerIsGrounded() && jumpCount >= maxJumps) return;
            jumpCount++;
            currentDownwardVelocity += jumpPower;
            isGliding = false;
        }
        
        // start gliding when user holds jump key after performing max amount of jumps
        else if (context.performed && jumpCount >= maxJumps)
        {
            isGliding = true;
            glideStart = Time.time;
        }

        // stop gliding when user releases the key
        else if (context.canceled)
        {
            isGliding = false;
        }
    }

    private void ApplyTilt()
    {
        if (glidePivot == null) return;

        // Target tilt rotation for the pivot during gliding
        Quaternion targetTilt = Quaternion.Euler(glideTiltAngle, 0, 0);
        glidePivot.localRotation = Quaternion.Lerp(glidePivot.localRotation, targetTilt, playerSpeed * Time.deltaTime);
    }

    private void ResetTilt()
    {
        if (glidePivot == null) return;

        // Smoothly reset pivot to original rotation when gliding stops
        glidePivot.localRotation = Quaternion.Lerp(glidePivot.localRotation, defaultPlayerRotation, playerSpeed * 3 * Time.deltaTime);
    }

    private bool PlayerIsGrounded()
    {
        bool isGrounded = characterController.isGrounded;
        if (isGrounded)
        {
            jumpCount = 0;
            isGliding = false;
        }
        return isGrounded;
    } 

}
