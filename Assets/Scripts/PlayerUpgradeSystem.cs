using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject[] inventorySprites;
    [SerializeField] private GameObject[] salengButton;

    private PlayerController playerController;
    private LevelManager levelManager;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        levelManager = LevelManager.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdatePlayerStats(); // Initial setup
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
            salengButton[currentUpgradeLevel].SetActive(false);
            currentUpgradeLevel++;
            if (currentUpgradeLevel != 2)
            {
                salengButton[currentUpgradeLevel].SetActive(true);
            }
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

        if (currentUpgradeLevel == 1)
        {
            inventorySprites[0].SetActive(true);
        }

        if (currentUpgradeLevel == 2)
        {
            inventorySprites[1].SetActive(true);
        }
    }
}