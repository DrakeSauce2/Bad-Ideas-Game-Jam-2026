using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerController playerController;

    //

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed             = 5f;
    [SerializeField] private float jumpHeight            = 5f;
    [SerializeField] private float gravity               = -9.81f;
    [SerializeField] private float turnSpeed             = 5f;
    [SerializeField] private float airControl            =  2f;
    [SerializeField] private float airDrag               = 0.8f;
    [SerializeField] private float maxAirSpeedMultiplier = 1.0f; 

    private Vector2 moveInput = Vector2.zero;
    private float verticalVelocity = 0f;
    private float yawInput = 0f;

    private Vector3 horizontalVelocity = Vector3.zero;

    //

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();

        playerController.onMove += HandleMove;
        playerController.onLook += HandleLook;

        playerController.onJump += Jump;
    }

    private void HandleMove(Vector2 moveInput) => this.moveInput = moveInput;

    private void HandleLook(Vector2 lookInput) => yawInput = lookInput.x;

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (!characterController) return;

        transform.Rotate(Vector3.up * yawInput * turnSpeed * Time.deltaTime);

       ApplyGravityForce();

        Vector3 desiredMove =
            transform.forward * moveInput.y +
            transform.right * moveInput.x;

        if (desiredMove.sqrMagnitude > 1f)
            desiredMove.Normalize();

        ApplyGroundVelocity(desiredMove);

        Vector3 velocity = horizontalVelocity + Vector3.up * verticalVelocity;

        characterController.Move(velocity * Time.deltaTime);
    }

    
    private void ApplyGroundVelocity(Vector3 desiredMove)
    {
        if (characterController.isGrounded)
        {
            horizontalVelocity = desiredMove * moveSpeed;
        }
        else
        {
            horizontalVelocity += desiredMove * moveSpeed * airControl * Time.deltaTime;

            float maxAirSpeed = moveSpeed * Mathf.Max(0.001f, maxAirSpeedMultiplier);
            if (horizontalVelocity.magnitude > maxAirSpeed) 
            {
                horizontalVelocity = horizontalVelocity.normalized * Mathf.Lerp(horizontalVelocity.magnitude, maxAirSpeed, 0.5f);
            }

            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, airDrag * Time.deltaTime);
        }
    }

    private void ApplyGravityForce()
    {
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }   

    private void Jump()
    {
        if (!characterController.isGrounded) return;

        Vector3 initial = transform.forward * moveInput.y + transform.right * moveInput.x;
        if (initial.sqrMagnitude > 1f) initial.Normalize();
        horizontalVelocity = initial * moveSpeed;

        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

}
