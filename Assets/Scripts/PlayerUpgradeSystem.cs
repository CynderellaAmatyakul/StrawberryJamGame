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
    [SerializeField] private int maxUpgradeLevel = 3;

    [Header("Sprite References")]
    [SerializeField] private Sprite[] playerSprites;

    [Header("UI References")]
    [SerializeField] private Button upgradeButton;

    private PlayerController playerController;
    private LevelManager levelManager;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        levelManager = LevelManager.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();

        upgradeButton.onClick.AddListener(AttemptUpgrade);
        UpdateUI();
    }

    private void AttemptUpgrade()
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
            UpdateUI();
        }
    }

    private void UpdatePlayerStats()
    {
        int newInventorySize = 1 + currentUpgradeLevel;
        playerController.inventory = new GameObject[newInventorySize];
        playerController.playerSpeed = basePlayerSpeed + (speedIncreasePerLevel * currentUpgradeLevel);

        // Change sprite based on upgrade level
        if (playerSprites.Length > currentUpgradeLevel)
        {
            spriteRenderer.sprite = playerSprites[currentUpgradeLevel];
        }
    }

    private void UpdateUI()
    {
        upgradeButton.interactable = currentUpgradeLevel < maxUpgradeLevel &&
                                     levelManager.currency >= upgradeCost;
    }
}