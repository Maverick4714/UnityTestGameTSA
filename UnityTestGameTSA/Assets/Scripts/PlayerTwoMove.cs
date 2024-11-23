using UnityEngine;

public class PlayerTwoMove : MonoBehaviour
{
    public float moveSpeed = 5f;    // Movement speed
    public float jumpForce = 5f;   // Force applied for jumping
    private bool isGrounded = true; // Check if player is grounded

    private Rigidbody rb;          // Reference to Rigidbody
    private KeyCode jumpKey = KeyCode.UpArrow;   // Key to jump
    private KeyCode moveLeftKey = KeyCode.LeftArrow;   // Key to move left
    private KeyCode moveRightKey = KeyCode.RightArrow;  // Key to move right
    private KeyCode moveBackwardKey = KeyCode.DownArrow; // Key to move backward

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing. Please attach one to the GameObject.");
        }
    }

    void Update()
    {
        // Horizontal movement
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
            move += Vector3.left;

        if (Input.GetKey(KeyCode.RightArrow))
            move += Vector3.right;

        if (Input.GetKey(KeyCode.DownArrow))
            move += Vector3.back;

        transform.position += move * moveSpeed * Time.deltaTime;

        // Jump logic
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent jumping until grounded
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}