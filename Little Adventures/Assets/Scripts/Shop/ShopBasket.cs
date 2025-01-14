using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Basket : MonoBehaviour
{
    private ShopController shopController;

    private void Start()
    {
        shopController = FindFirstObjectByType<ShopController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool additionResult = shopController.OnFruitCollected(other.tag);
        if (additionResult)
        {
            // remove fruit after adding successfully
            Destroy(other.gameObject); 
        }
    }
}
