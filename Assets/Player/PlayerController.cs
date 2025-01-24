using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 _moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = _moveInput * moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput =context.ReadValue<Vector2>();
    }
}
