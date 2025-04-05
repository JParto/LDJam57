using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Vector2 movementInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // Enable input actions if needed
    }

    private void OnDisable()
    {
        // Disable input actions if needed
    }

    // Called by the Input System when movement input is detected
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        // Apply movement
        Vector2 move = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = move;
        
    }
}