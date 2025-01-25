using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform startPoint;
    public Transform[] path;

    public int currency;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currency = 1000;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("<color=red>You are so poor</color>");
            return false;
        }
    }
}
