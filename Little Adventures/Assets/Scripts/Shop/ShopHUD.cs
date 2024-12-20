using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopHUD : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI fruitCountText;
    [SerializeField] TextMeshProUGUI gameOverText;

    public void updateCounts(Dictionary<string, int> currentCounts, Dictionary<string, int> targetCounts)
    {
        string countsString = "";
        foreach(KeyValuePair<string, int> fruitCount in targetCounts)
        {
             countsString += fruitCount.Key + "s: " + currentCounts.GetValueOrDefault(fruitCount.Key, 0) + "/" + fruitCount.Value + "<br>";
        }
        fruitCountText.text = countsString;
    }

    public void showGameOverText()
    {
        gameOverText.text = "Well done, you have found all necessary fruits and vegetables.<br>You can leave the shop by pressing Escape.";
    }
}
