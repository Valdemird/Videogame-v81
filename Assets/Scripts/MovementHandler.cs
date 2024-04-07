using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementHandler : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float AirMoveSpeed;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private float WeaponRange;

    [SerializeField] private UnitProperties playerProperties;
    private float Gravity =  Physics.gravity.y;

    // Private fields
    private CharacterController controller;
    private LineRenderer lineRenderer;
    private Vector3 lookDirection;
    private Vector3 velocity;
    private bool isJumping = false;
    private bool isAiming = false;
    private Animator animator;


    // Animator parameters
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsAiming = Animator.StringToHash("IsAiming");

    private void Awake()
    {

        if (playerProperties != null)
        {
            MoveSpeed = playerProperties.MoveSpeed;
            AirMoveSpeed = playerProperties.AirMoveSpeed;
            JumpHeight = playerProperties.JumpHeight;
            RotationSpeed = playerProperties.RotationSpeed;
            WeaponRange = playerProperties.WeaponRange;
        }
        else
        {
            Debug.LogError("PlayerProperties is not assigned in the inspector.");
        }
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController component not found on this game object.");
        }

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component not found on this game object.");
        }

        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this game object.");
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleAiming();
        HandleRotation();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        float currentMoveSpeed = controller.isGrounded ? MoveSpeed : AirMoveSpeed;

        controller.Move((movement * currentMoveSpeed + velocity) * Time.deltaTime);

        animator.SetBool(IsMoving, movement.magnitude > 0);
    }

    private void HandleJumping()
    {
        if (controller.isGrounded)
        {
            velocity.y = -2f;

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            else
            {
                animator.SetBool(IsJumping, false);
            }
        }

        velocity.y += Gravity * Time.deltaTime;

        if (controller.isGrounded && isJumping)
        {
            isJumping = false;
        }
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        isJumping = true;
        animator.SetBool(IsJumping, true);
    }

    private void HandleAiming()
    {
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartAiming();
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                StopAiming();
            }
        }
    }

    private void StartAiming()
    {
        animator.SetBool(IsAiming, true);
        isAiming = true;

        lineRenderer.enabled = true;
    }

    private void StopAiming()
    {
        animator.SetBool(IsAiming, false);
        isAiming = false;
        lineRenderer.enabled = false;
    }

    private void HandleRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane floorPlane = new Plane(Vector3.up, Vector3.zero); // Plane representing the floor (y = 0)

        if (floorPlane.Raycast(ray, out var rayDistance))
        {
            Vector3 hitPoint = ray.GetPoint(rayDistance); // This is the point on the floor where the mouse is pointing

            // Use a fixed y-coordinate for the hit point
            hitPoint.y = 0f;

            Vector3 lookDirection = hitPoint - transform.position;
            lookDirection.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

            // Debug lines
            Debug.DrawLine(ray.origin, hitPoint, Color.red); 
            Debug.DrawRay(transform.position, lookDirection, Color.blue);

            // Handle aiming line rendering
            if(isAiming)
                HandleAimingLineRendering(lookDirection);
        }
    }

    private void HandleAimingLineRendering(Vector3 lookDirection)
    {
        // Set the lineRenderer to point towards the mouse position
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, 0, transform.position.z));

        // Set the end point at a fixed distance
        Vector3 endPoint = transform.position + lookDirection.normalized * WeaponRange;
        if (Physics.Raycast(transform.position, lookDirection, out var hit, WeaponRange)) // Use weaponRange here
        {
            // If it hits a target, set the end point at the hit point
            lineRenderer.SetPosition(1, new Vector3(hit.point.x, 0, hit.point.z));
        }
        else
        {
            lineRenderer.SetPosition(1, new Vector3(endPoint.x, 0, endPoint.z));
        }
    }
}