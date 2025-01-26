using UnityEngine;
using System.Collections;


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
    [SerializeField] private float dashCooldown = 1f;            // Cooldown time for dashing
    [SerializeField] private float dashSpeedMultiplier = 7f; // How much faster the player moves during a dash
    [SerializeField] private float dashDuration = 0.1f;      // Duration of the dash in seconds

    [Header("Melee Attack Settings")]
    [SerializeField] private float meleeCooldown = 0.7f; // Cooldown time between melee attacks
    [SerializeField] private GameObject meleeEffectPrefab; // Prefab for melee effect (e.g., slash sprite)
    [SerializeField] private float offsetDistance = 1.0f; // kaugel attack spawning
    private bool isDashing = false;  
    private bool isMelee = false;                                // Tracks current mode (false = ranged, true = melee)
    private float shootTimer = 0f;       
    private float meleeTimer = 0f;                         // Timer for shooting cooldown
    private float transformationTimer = 0f;                     // Timer for transformation cooldown
    private float dashTimer = 0f;                                // Timer for dash cooldown

    private Vector2 moveInput;                                   // Input for movement
    private Vector2 mousePosition;                               // Mouse position for aiming

    private SpriteRenderer spriteRenderer; 

    private bool isWalking = false;

    [SerializeField] private ParticleSystem runParticles; // Reference to the particle system

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
        HandleMeleeAttack();
    }

    private void HandleMovement() {
        // Player movement input
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float currentSpeed = isMelee ? meleeSpeed : rangedSpeed;
        transform.Translate(moveInput * currentSpeed * Time.deltaTime, Space.World);
        

        // edge collision checks 
        if(transform.position.x < -9) transform.position = new Vector3(-9, transform.position.y, transform.position.z);
        if(transform.position.x > 9) transform.position = new Vector3(9, transform.position.y, transform.position.z);
        if (transform.position.y < -9) transform.position = new Vector3(transform.position.x, -9, transform.position.z);
        if (transform.position.y > 9) transform.position = new Vector3(transform.position.x, 9, transform.position.z);


        // Rotate the player towards the mouse
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        //transform.up = direction;
        firePoint.up = direction;
        // Flip sprite based on movement direction
        if (moveInput.x != 0) {
            spriteRenderer.flipX = moveInput.x < 0;
        }
        // Handle particle effects
    if (moveInput.magnitude > 0 && !runParticles.isPlaying) {
        runParticles.Play(); // Play particles when moving
    } else if (moveInput.magnitude == 0 && runParticles.isPlaying) {
        runParticles.Stop(); // Stop particles when idle
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

    private void HandleMeleeAttack() {
        // Reduce the melee cooldown timer
        if (meleeTimer > 0) {
            meleeTimer -= Time.deltaTime;
        }

        // Perform melee attack when in melee mode and cooldown allows
        if (isMelee && Input.GetMouseButtonDown(0) && meleeTimer <= 0) {
            animator.SetTrigger("MeleeAttack"); // Trigger attack animation
            PerformMeleeEffect(); // Visualize the melee attack
            meleeTimer = meleeCooldown; // Reset melee cooldown
        }
    }

    private void PerformMeleeEffect() {
        // Spawn a melee effect (e.g., slash sprite) at the firePoint
        if (meleeEffectPrefab != null) {
            // Calculate the offset based on the firePoint's rotation
            Vector3 offset = firePoint.up * offsetDistance;

            // Spawn the slash effect with the offset
            Vector3 spawnPosition = firePoint.position + offset;
            spawnPosition.z = -0.5f;
            Instantiate(meleeEffectPrefab, spawnPosition, firePoint.rotation);

            Debug.Log("Slash effect spawned!");
            } else {
            Debug.LogWarning("SlashEffectPrefab is not assigned!");
            }
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
    private void Dash() {
        if (!isDashing) StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine() {
        isDashing = true; // Set dashing to true

        float originalSpeed = isMelee ? meleeSpeed : rangedSpeed; // Save the current speed
        float boostedSpeed = originalSpeed * dashSpeedMultiplier; // Calculate boosted speed

        if (isMelee) {
            meleeSpeed = boostedSpeed; // Temporarily increase melee speed
        } else {
            rangedSpeed = boostedSpeed; // (Optional) Adjust ranged speed if needed
        }

        Debug.Log("Started dashing!");

        yield return new WaitForSeconds(dashDuration); // Wait for the dash duration

        if (isMelee) {
            meleeSpeed = originalSpeed; // Restore original melee speed
        } else {
            rangedSpeed = originalSpeed; // Restore ranged speed if changed
        }

        isDashing = false; // Reset dashing
        Debug.Log("Finished dashing!");
    }

    private void HandleDash() {
    // Reduce the dash cooldown timer
        if (dashTimer > 0) {
            dashTimer -= Time.deltaTime;
        }

        // Allow dashing if cooldown has finished
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
    /*
    private void Dash() {
        Vector3 dashVector = firePoint.up * dashDistance; // Dash in the direction the player is facing
        transform.position += dashVector;
        Debug.Log("Dashed forward!");
    }*/

    private void HandleAnimations() {
        // Update animation parameters
        // isWalking = moveInput.magnitude > 0;  True if player is moving
        isWalking = Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0;
        animator.SetBool("isWalking", isWalking);
    }
}
