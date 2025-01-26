using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public float playerSpeed = 3f;
    [SerializeField] public GameObject[] inventory = new GameObject[1];
    [SerializeField] public Image[] inventorySlots;
    private Vector2 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Plot"))
            {
                targetPosition = mousePosition;
            }
        }

        // Move towards target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * playerSpeed);

        // Rotate to face mouse direction
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public bool AddToInventory(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;

                // Comprehensive null checks
                if (inventorySlots == null || inventorySlots.Length == 0)
                {
                    Debug.LogWarning("Inventory slots are not assigned!");
                    return true;
                }

                if (i >= inventorySlots.Length)
                {
                    Debug.LogWarning($"Inventory slot index {i} is out of range!");
                    return true;
                }

                // Safe sprite retrieval
                SpriteRenderer itemSprite = item.GetComponentInChildren<SpriteRenderer>(true);
                if (itemSprite == null)
                {
                    Debug.LogWarning($"No SpriteRenderer found in {item.name}");
                    return true;
                }

                // Safe UI slot assignment
                if (inventorySlots[i] != null)
                {
                    inventorySlots[i].sprite = itemSprite.sprite;
                    inventorySlots[i].enabled = true;
                }
                else
                {
                    Debug.LogWarning($"Inventory slot {i} is null!");
                }

                return true;
            }
        }
        return false;
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                if (inventorySlots[i] != null)
                {
                    inventorySlots[i].sprite = null;
                    inventorySlots[i].enabled = false;
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup"))
        {
            if (AddToInventory(other.gameObject))
            {
                // Instead of destroying, just disable the object
                other.gameObject.SetActive(false);
            }
        }
    }
}
