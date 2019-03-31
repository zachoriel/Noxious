using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    [SerializeField] float jumpForce = 5f;
    bool isGrounded, canJump;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                canJump = true;
            }
        }
    }

    void FixedUpdate()
    {
        // Jumping physics
        #region Jumping
        if (canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        #endregion
    }

    // Jumping conditions checking & applying
    #region Jump Condition Check
    // If player is connected to ground, jumping is enabled
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canJump = false;
        }
    }

    // If player is not connected to ground, jumping is disabled
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            canJump = false;
        }
    }
    #endregion
}
