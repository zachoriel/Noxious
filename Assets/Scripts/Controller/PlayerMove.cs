using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
   This player controller was made during the very early stages of
   development, when the design and scope of the game were significantly
   different. As such, the controller has some unnecessary features
   (such as jumping, raycasted slope handling, walking/running, etc). 
*/

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;

    [Header("Player Movement")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    float appliedMovementSpeed;
    [SerializeField] float runBuildUpSpeed;
    string horizontal = "Horizontal";
    string vertical = "Vertical";
    KeyCode runKey = KeyCode.LeftShift;

    [Header("Jumping Mechanic")]
    [SerializeField] float jumpMultiplier;
    [SerializeField] AnimationCurve jumpFallOff;
    KeyCode jumpKey = KeyCode.Space;
    bool isJumping;

    [Header("Slope Handling")]
    [SerializeField] float slopeForceToApply;
    [SerializeField] float slopeCheckRayLength;

	void Awake ()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }
	}
	
	void Update ()
    {
        Movement();
	}

    void Movement()
    {
        float horizInput = Input.GetAxis(horizontal);
        float vertInput = Input.GetAxis(vertical);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        controller.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1f) * appliedMovementSpeed);

        if ((vertInput != 0 || horizInput != 0) && OnSlope())
        {
            controller.Move(Vector3.down * controller.height / 2 * slopeForceToApply * Time.deltaTime);
        }

        SetMovementSpeed();
        JumpInput();
    }

    void SetMovementSpeed()
    {
        if (Input.GetKey(runKey) && PlayerData.instance.stamina > 0)
        {
            appliedMovementSpeed = Mathf.Lerp(appliedMovementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
            PlayerData.instance.running = true;
        }
        else
        {
            appliedMovementSpeed = Mathf.Lerp(appliedMovementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
            PlayerData.instance.running = false;
        }
    }

    bool OnSlope()
    {
        if (isJumping)
        {
            return false;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeCheckRayLength))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }

        return false;
    }

    void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    IEnumerator JumpEvent()
    {
        controller.slopeLimit = 90f;
        float timeInAir = 0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);

        controller.slopeLimit = 45f;
        isJumping = false;
    }
}
