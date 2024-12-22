using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string targetSceneName;
    [SerializeField] private Transform cityReturnPoint; // position in the city scene

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cityReturnPoint != null)
            {
                // Set the return position to remebmer after transition back into the city
                PlayerPositionManager.SetReturnPosition(cityReturnPoint.position); 
            }
            SceneManager.LoadScene(targetSceneName);
        }
    }

}
