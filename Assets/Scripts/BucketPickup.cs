using UnityEngine;

public class BucketPickup : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private int currencyInterval = 100;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController playerController = other.GetComponent<PlayerController>();
        for (int i = 0; i < playerController.inventory.Length; i++)
        {
            if (playerController.inventory[i] != null &&
                playerController.inventory[i].name == "Bucket of boba(Clone)")
            {
                // Increase currency
                LevelManager.instance.currency += currencyInterval;

                // Destroy the item from inventory
                Destroy(playerController.inventory[i]);
                playerController.inventory[i] = null;
                playerController.UpdateInventoryUI();

                break;
            }
        }
    }
}