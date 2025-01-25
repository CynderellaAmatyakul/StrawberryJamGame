using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float playerSpeed = 2f;
    [SerializeField] public GameObject[] inventory = new GameObject[3];
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
                // Store a reference to the prefab
                inventory[i] = item.gameObject;
                return true;
            }
        }
        return false; // Inventory is full
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
