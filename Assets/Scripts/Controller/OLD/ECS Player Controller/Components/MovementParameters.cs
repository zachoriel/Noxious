using UnityEngine;

public class MovementParameters : MonoBehaviour
{
    //[Header("Input")]
    [HideInInspector] public float verticalMovement = 0f;
    [HideInInspector] public float horizontalMovement = 0f;

    [Header("Speeds")]
    public float moveSpeed = 10f;
    public float backwardSpeed = 5f;
    public float sprintSpeed = 20f;
}
