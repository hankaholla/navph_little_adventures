using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    private ShopHUD shopHUD;
    [SerializeField] private string citySceneName;

    [SerializeField] private GameObject[] fruitPrefabs;
    [SerializeField] private int fruitGridSize = 5;
    private Vector3[] spawnPoints;

    private Dictionary<string, int> totalCounts = new Dictionary<string, int>();
    private Dictionary<string, int> targetCounts = new Dictionary<string, int>();
    private Dictionary<string, int> currentCounts = new Dictionary<string, int>();

    public void Start()
    {
        shopHUD = FindFirstObjectByType<ShopHUD>();

        // generate fruit spawn points in a grid
        spawnPoints = new Vector3[fruitGridSize * fruitGridSize];
        int i = 0;
        for (int x = 0; x < fruitGridSize; x++)
        {
            for (int y = 0; y < fruitGridSize; y++)
            {
                spawnPoints[i] = new Vector3(x, y + 1, 0);
                i++;
            }
        }

        foreach (var spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, fruitPrefabs.Length);
            GameObject obj = Instantiate(fruitPrefabs[randomIndex], spawnPoint, Quaternion.identity);
            totalCounts[obj.tag] = totalCounts.GetValueOrDefault(obj.tag, 0) + 1;
        }

        // randomly compute the target counts
        foreach(KeyValuePair<string, int> p in totalCounts)
        {
            targetCounts[p.Key] = UnityEngine.Random.Range(1, p.Value);
            currentCounts[p.Key] = 0;
        }

        // show inital state
        shopHUD.updateCounts(currentCounts, targetCounts);
    }

    public bool OnFruitCollected(string fruit)
    {
        if (currentCounts[fruit] < targetCounts[fruit])
        {
            currentCounts[fruit] += 1;
            shopHUD.updateCounts(currentCounts, targetCounts);
            if (GameOver())
            {
                shopHUD.showGameOverText();
            }
            return true;  // fruit was added
        }
        return false; // fruit is not needed
    }

    private bool GameOver()
    {
        foreach(KeyValuePair<string, int> fruitCount in targetCounts)
        {
            if (currentCounts.GetValueOrDefault(fruitCount.Key, 0) < fruitCount.Value)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        // exit the shop when user presses X
         if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene(citySceneName);
        }
    }

}
