using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.WorldToScreenPoint(transform.position).z; // Keep the depth consistent
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
