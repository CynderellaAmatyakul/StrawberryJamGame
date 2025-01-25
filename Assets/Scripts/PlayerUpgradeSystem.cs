using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgradeSystem : MonoBehaviour
{
    [Header("Upgrade Settings")]
    [SerializeField] private int upgradeCost = 700;
    [SerializeField] private float basePlayerSpeed = 2f;
    [SerializeField] private float speedIncreasePerLevel = 1f;

    [Header("Progression")]
    [SerializeField] private int currentUpgradeLevel = 0;
    [SerializeField] private int maxUpgradeLevel = 2;

    [Header("Sprite References")]
    [SerializeField] private Sprite[] playerSprites;

    private PlayerController playerController;
    private LevelManager levelManager;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        levelManager = LevelManager.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AttemptUpgrade()
    {
        if (currentUpgradeLevel >= maxUpgradeLevel)
        {
            Debug.Log("Max upgrade level reached!");
            return;
        }

        if (levelManager.SpendCurrency(upgradeCost))
        {
            currentUpgradeLevel++;
            UpdatePlayerStats();
        }
    }

    private void UpdatePlayerStats()
    {
        int newInventorySize = 2 + currentUpgradeLevel;
        playerController.inventory = new GameObject[newInventorySize];
        playerController.playerSpeed = basePlayerSpeed + (speedIncreasePerLevel * currentUpgradeLevel);

        if (playerSprites.Length > currentUpgradeLevel)
        {
            spriteRenderer.sprite = playerSprites[currentUpgradeLevel];
        }
    }
}