using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;

    private Vector2 movementInput;
    private Rigidbody2D rb;

    public LayerTransporter transporter;

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

    public void OnJump(InputValue value)
    {
        if (IsGrounded() && value.isPressed && transporter == null)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        else if (transporter != null && value.isPressed)
        {
            // Handle the jump input when the player is in a transporter
            transporter.TriggerTransport();
        }
    }

    private bool IsGrounded()
    {
        // Check if the player is grounded using a raycast or collider check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }

    private void FixedUpdate()
    {
        // Apply movement
        Vector2 move = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = move;
        
    }
}