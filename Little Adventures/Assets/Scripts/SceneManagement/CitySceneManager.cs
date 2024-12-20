using UnityEngine;

public class CitySceneManager : MonoBehaviour
{

    // place the player on his previous position in the city after returning from other scenes (shop, house, ...)
    private void Start()
    {
        if (PlayerPositionManager.ReturnPosition.HasValue)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = PlayerPositionManager.ReturnPosition.Value;
            }
        }
    }
}
