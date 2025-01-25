using UnityEngine;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour
{
    public List<string> collectedCards = new List<string>(); // List of collected cards
    public GameObject turretPrefab; // Prefab to instantiate when placing a turret
    private string selectedCard; // Currently selected card

    public void CollectCard(string cardType)
    {
        collectedCards.Add(cardType);
        Debug.Log($"Collected Card: {cardType}");
        UpdateCardUI();
    }

    public void SelectCard(string cardType)
    {
        if (collectedCards.Contains(cardType))
        {
            selectedCard = cardType;
            Debug.Log($"Selected Card: {cardType}");
        }
        else
        {
            Debug.Log("Card not available.");
        }
    }

    public bool UseCard(string cardType)
    {
        if (collectedCards.Contains(cardType))
        {
            collectedCards.Remove(cardType);
            Debug.Log($"Used Card: {cardType}");
            UpdateCardUI();
            return true;
        }
        return false;
    }

    private void UpdateCardUI()
    {
        // Update the UI to display available cards (e.g., using buttons or images)
        Debug.Log($"Updated UI with cards: {string.Join(", ", collectedCards)}");
    }

    public string GetSelectedCard()
    {
        return selectedCard;
    }
}
