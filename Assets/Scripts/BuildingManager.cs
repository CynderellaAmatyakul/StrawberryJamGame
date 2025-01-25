using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Tower[] towers;

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
}
