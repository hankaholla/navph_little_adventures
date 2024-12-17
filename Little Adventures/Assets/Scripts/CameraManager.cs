using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float distanceToPlayer;

    private Vector2 input;

    [SerializeField] private MouseSensitivity mouseSensitivity;
    [SerializeField] private CameraAngle cameraAngle;

    private CameraRotation cameraRotation;

    private void Awake()
    {
        // remember initial distance from player to camera
        distanceToPlayer = Vector3.Distance(transform.position, target.position);
    }

    public void Look(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void Update()
    {
        cameraRotation.yaw += input.x * mouseSensitivity.horizontal * BoolToInt(mouseSensitivity.invertHorizontal) * Time.deltaTime;
        cameraRotation.pitch += input.y * mouseSensitivity.vertical * BoolToInt(mouseSensitivity.invertVertical) * Time.deltaTime;
        cameraRotation.pitch = Mathf.Clamp(cameraRotation.pitch, cameraAngle.min, cameraAngle.max);
    }

    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(cameraRotation.pitch, cameraRotation.yaw, 0.0f);
        transform.position = target.position - transform.forward * distanceToPlayer;
    }

    private static int BoolToInt(bool b) => b ? 1 : -1;
}

[Serializable]
public struct MouseSensitivity
{
    public float horizontal;
    public float vertical;
    public bool invertHorizontal;
    public bool invertVertical;
}

public struct CameraRotation
{
    public float pitch; // x-axis
    public float yaw; // y-axis
}

[Serializable]
public struct CameraAngle
{
    public float min;
    public float max;
}
