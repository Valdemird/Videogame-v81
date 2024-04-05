using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the character movement
    public float airMoveSpeed = 2f; // Speed of the character movement while in the air
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 2f; // Height of the jump
    public float rotationSpeed = 5f; // Speed of rotation based on mouse movement
    private Animator animator; // Reference to the Animator component
    private CharacterController controller; // Reference to the CharacterController component
    private Vector3 velocity; // Current velocity of the character

    private bool isJumping = false; // Flag to track whether the character is jumping

    void Start()
    {
        // Get the CharacterController component attached to the character GameObject
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>(); // Get the Animator component
    }

    void Update()
    {
        // Get the horizontal and vertical input from the player (arrow keys, WASD, etc.)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector based on the input
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        if (movement.magnitude > 0)
        {
            // Set the "Moving" parameter to true if the character is moving
            animator.SetBool("moving", true);
        }
        else
        {
            // Set the "Moving" parameter to false if the character is not moving
            animator.SetBool("moving", false);
        }
        // Determine move speed based on whether the character is grounded or in the air
        float currentMoveSpeed = controller.isGrounded ? moveSpeed : airMoveSpeed;

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Jumping
        if (controller.isGrounded)
        {
            // Reset velocity when grounded
            velocity.y = -2f;

            if (Input.GetButtonDown("Jump"))
            {
                // Apply jump force
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isJumping = true;
            }
        }

        // Cast a ray from the camera towards the mouse cursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits something
        if (Physics.Raycast(ray, out hit))
        {
            // Get the direction from the character's position to the hit point
            Vector3 lookDirection = hit.point - transform.position;
            lookDirection.y = 0f; // Ensure the character doesn't tilt up or down

            // Rotate the character to face the hit point direction smoothly
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Move the character based on the movement vector and current move speed
        controller.Move((movement * currentMoveSpeed + velocity) * Time.deltaTime);

        // If the character is grounded and was jumping, reset the jump flag
        if (controller.isGrounded && isJumping)
        {
            isJumping = false;
        }
    }
}
