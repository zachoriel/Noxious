//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[System.Serializable]
//public struct MovementData
//{
//    public float moveSpeed;
//    public float backwardSpeed;
//    public float sprintSpeed;
//    public float jumpForce;
//}

//public class PlayerController : MonoBehaviour
//{
//    [Header("Player Movement Settings")]
//    public MovementData movement;

//    // Input variables for movement
//    float verticalMovement = 0f;
//    float horizontalMovement = 0f;
//    bool isGrounded, canJump; // isGrounded is for input, canJump is for applying physics in FixedUpdate

//    // Scripts dependencies and components
//    PlayerStats playerStats;
//    Transform playerTransform;
//    Rigidbody rb;

//    void Start()
//    {
//        FindComponents(); // See below
//    }

//    void Update()
//    {
//        // Input check for movement
//        verticalMovement = Input.GetAxisRaw("Vertical");
//        horizontalMovement = Input.GetAxisRaw("Horizontal");

//        // Input check for jumping
//        if (isGrounded)
//        {
//            if (Input.GetButtonDown("Jump"))
//            {
//                canJump = true;
//            }
//        }
//    }

//    void FixedUpdate()
//    {
//        // Vertical movement physics
//        #region Up & Down Movement
//        // Normal forward movement (normal speed)
//        if (verticalMovement > 0)
//        {
//            playerTransform.position += playerTransform.forward * movement.moveSpeed * Time.deltaTime;
//            playerStats.running = false;
//        }
//        // Fast forward movement (sprinting) & player has stamina
//        if (verticalMovement > 0 && Input.GetKey(KeyCode.LeftShift) && playerStats.player.stamina > 0)
//        {
//            playerTransform.position += playerTransform.forward * movement.sprintSpeed * Time.deltaTime;
//            playerStats.UseStamina();
//            playerStats.running = true;
//        }
//        // Player has no stamina
//        else if (playerStats.player.stamina <= 0 || verticalMovement == 0)
//        {
//            playerStats.running = false;
//        }

//        // Normal backwards movement (slowed)
//        if (verticalMovement < 0)
//        {
//            playerTransform.position -= playerTransform.forward * movement.backwardSpeed * Time.deltaTime;
//            playerStats.running = false;
//        }
//        // Fast backwards movement (normal speed) & player has stamina
//        if (verticalMovement < 0 && Input.GetKey(KeyCode.LeftShift) && playerStats.player.stamina > 0)
//        {
//            playerTransform.position -= playerTransform.forward * movement.moveSpeed * Time.deltaTime;
//            playerStats.UseStamina();
//            playerStats.running = true;
//        }
//        // Player has no stamina
//        else if (playerStats.player.stamina <= 0 || verticalMovement == 0)
//        {
//            playerStats.running = false;
//        }
//        #endregion

//        // Horizontal movement physics
//        #region Sideways Movement
//        // Normal right movement (normal speed)
//        if (horizontalMovement > 0)
//        {
//            playerTransform.position += playerTransform.right * movement.moveSpeed * Time.deltaTime;
//        }

//        // Normal left movement (normal speed)
//        if (horizontalMovement < 0)
//        {
//            playerTransform.position -= playerTransform.right * movement.moveSpeed * Time.deltaTime;
//        }
//        #endregion

//        // Jumping physics
//        #region Jumping
//        if (canJump)
//        {
//            rb.AddForce(Vector3.up * movement.jumpForce, ForceMode.Impulse);
//        }
//        #endregion
//    }

//    // Jumping conditions checking & applying
//    #region Jump Condition Check
//    // If player is connected to ground, jumping is enabled
//    void OnCollisionEnter(Collision other)
//    {
//        if (other.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = true;
//            canJump = false;
//        }
//    }

//    // If player is not connected to ground, jumping is disabled
//    void OnCollisionExit(Collision other)
//    {
//        if (other.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = false;
//            canJump = false;
//        }
//    }
//    #endregion

//    // Finds components to be applied on Start because IT LOOKS CLEANER UP THERE AND I LIKE IT, OKAY? 
//    void FindComponents()
//    {
//        playerTransform = GetComponent<Transform>();
//        rb = GetComponent<Rigidbody>();
//        playerStats = FindObjectOfType<PlayerStats>();
//    }
//}