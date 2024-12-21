using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public static Vector3? ReturnPosition { get; set; }

    // Method to set the return position of the player after entering a building
    public static void SetReturnPosition(Vector3 position)
    {
        ReturnPosition = position;
    }
}
