using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] public float distanceToPlayer = 15f;
    [SerializeField] private float minDistanceToPlayer = 5f;
    [SerializeField] private float maxDistanceToPlayer = 50f;
    private float zoomDelta;

    private Vector2 cameraLookInput;

    [SerializeField] private MouseSensitivity mouseSensitivity;
    [SerializeField] private CameraAngle cameraAngle;
    private CameraRotation cameraRotation;

    public void Look(InputAction.CallbackContext context)
    {
        cameraLookInput = context.ReadValue<Vector2>();
    }

    public void Zoom(InputAction.CallbackContext context)
    {
        zoomDelta += context.ReadValue<Vector2>().y;
    }

    public void Update()
    {
        // Update camera rotation
        cameraRotation.yaw += cameraLookInput.x * mouseSensitivity.horizontal * BoolToInt(mouseSensitivity.invertHorizontal) * Time.deltaTime;
        cameraRotation.pitch += cameraLookInput.y * mouseSensitivity.vertical * BoolToInt(mouseSensitivity.invertVertical) * Time.deltaTime;
        cameraRotation.pitch = Mathf.Clamp(cameraRotation.pitch, cameraAngle.min, cameraAngle.max);
    }

    private void LateUpdate()
    {
        // Apply rotation and position changes
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
