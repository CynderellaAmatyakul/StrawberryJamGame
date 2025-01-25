using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Tower[] towers;
    [SerializeField] private Transform spawnUnit;

    private int selectedTower = 0;

    private void Awake()
    {
        Instance = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }

    public void SpawnUnit()
    {
        Tower selectedTowerType = GetSelectedTower();

        if (selectedTowerType != null && LevelManager.instance.currency >= selectedTowerType.cost)
        {
            GameObject unitInstance = Instantiate(selectedTowerType.prefab, spawnUnit.position, Quaternion.identity);
            unitInstance.tag = "Pickup";
            LevelManager.instance.SpendCurrency(selectedTowerType.cost);
        }
    }
}
