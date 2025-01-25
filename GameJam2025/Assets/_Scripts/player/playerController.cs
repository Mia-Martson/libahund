using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Animation Controllers")]
    [SerializeField] private RuntimeAnimatorController rangedController; // Ranged mode animations
    [SerializeField] private RuntimeAnimatorController meleeController;  // Melee mode animations

    private Animator animator;       // Reference to the Animator
    [Header("Movement Settings")]
    [SerializeField] private float rangedSpeed = 5f;   // Movement speed in ranged mode
    [SerializeField] private float meleeSpeed = 8f;    // Movement speed in melee mode

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;  // Prefab for ranged bullets
    [SerializeField] private Transform firePoint;      // Point where bullets are spawned
    [SerializeField] private float shootCooldown = 0.5f;

    [Header("Transformation Settings")]
    [SerializeField] private float transformationCooldown = 5f;  // Cooldown time for transforming

    [Header("Melee Dash Settings")]
    [SerializeField] private float dashDistance = 5f;            // Distance covered by the dash
    [SerializeField] private float dashCooldown = 1f;            // Cooldown time for dashing

    private bool isMelee = false;                                // Tracks current mode (false = ranged, true = melee)
    private float shootTimer = 0f;                               // Timer for shooting cooldown
    private float transformationTimer = 0f;                     // Timer for transformation cooldown
    private float dashTimer = 0f;                                // Timer for dash cooldown

    private Vector2 moveInput;                                   // Input for movement
    private Vector2 mousePosition;                               // Mouse position for aiming

    private SpriteRenderer spriteRenderer; 

    private bool isWalking = false;
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Get the Animator component
        SetMode(false); // Start in ranged mode
    }

    void Update() {
        HandleMovement();
        HandleAnimations();
        HandleShooting();
        HandleTransformation();
        HandleDash();
    }

    private void HandleMovement() {
        // Player movement input
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float currentSpeed = isMelee ? meleeSpeed : rangedSpeed;
        transform.Translate(moveInput * currentSpeed * Time.deltaTime, Space.World);

        // Rotate the player towards the mouse
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        //transform.up = direction;
        firePoint.up = direction;
        // Flip sprite based on movement direction
        if (moveInput.x != 0) {
            spriteRenderer.flipX = moveInput.x < 0;
        }
    }

    private void HandleShooting() {
        // Reduce the shooting cooldown timer
        if (shootTimer > 0) {
            shootTimer -= Time.deltaTime;
        }

        // Only allow shooting in ranged mode
        if (!isMelee && Input.GetMouseButton(0) && shootTimer <= 0) {
            Shoot();
            shootTimer = shootCooldown; // Reset shooting cooldown
        }
    }
    private void Shoot() {
        // Instantiate the bullet with the firePoint's position and rotation
        GameObject arrow = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Debug.Log("Bullet Fired!");
    }

    private void HandleTransformation() {
        // Reduce the transformation cooldown timer
        if (transformationTimer > 0) {
            transformationTimer -= Time.deltaTime;
        }

        // Transform when E is pressed and cooldown allows
        if (Input.GetKeyDown(KeyCode.E) && transformationTimer <= 0) {
            isMelee = !isMelee; // Toggle between ranged and melee
            SetMode(isMelee);
            transformationTimer = transformationCooldown; // Reset transformation cooldown

            Debug.Log($"Transformed to {(isMelee ? "Melee" : "Ranged")} mode!");
        }
    }

    private void HandleDash() {
        // Reduce the dash cooldown timer
        if (dashTimer > 0) {
            dashTimer -= Time.deltaTime;
        }

        // Only allow dashing in melee mode
        if (isMelee && Input.GetKeyDown(KeyCode.Space) && dashTimer <= 0) {
            Dash();
            dashTimer = dashCooldown; // Reset dash cooldown
        }
    }

    private void SetMode(bool meleeMode) {
        // Update the Animator Controller
        animator.runtimeAnimatorController = meleeMode ? meleeController : rangedController;

        // Update other properties (like speed or weapons) if needed
        Debug.Log($"Switched to {(meleeMode ? "Melee" : "Ranged")} mode!");
    }

    private void Dash() {
        Vector3 dashVector = firePoint.up * dashDistance; // Dash in the direction the player is facing
        transform.position += dashVector;
        Debug.Log("Dashed forward!");
    }

    private void HandleAnimations() {
        // Update animation parameters
        // isWalking = moveInput.magnitude > 0;  True if player is moving
        isWalking = Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0;
        animator.SetBool("isWalking", isWalking);
    }
}
