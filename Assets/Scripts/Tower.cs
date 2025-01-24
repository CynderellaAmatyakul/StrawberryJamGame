using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tower
{
    public string towerName;
    public int cost;
    public GameObject prefab;

    public Tower(string name, int cost, GameObject prefab)
    {
        this.towerName = name;
        this.cost = cost;
        this.prefab = prefab;
    }
}
