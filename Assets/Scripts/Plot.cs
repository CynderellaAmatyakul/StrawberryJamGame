using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;

    private GameObject tower;
    private Color startColor;

    private GameObject target;

    private void Start()
    {
        startColor = sr.color;

        target = GameObject.FindWithTag("Player");
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        Tower towerToBuild = BuildingManager.Instance.GetSelectedTower();
        if (towerToBuild.cost > LevelManager.instance.currency)
        {
            return;
        }

        if (target == null)
        {
            target = GameObject.FindWithTag("Player");
        }

        if (target != null && CheckTargetIsInRange())
        {
            LevelManager.instance.SpendCurrency(towerToBuild.cost);
            tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.transform.position, transform.position) <= targetingRange;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
