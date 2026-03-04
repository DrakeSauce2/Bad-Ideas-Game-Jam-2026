using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputActions inputActions;

    public delegate void OnMoveAction(Vector2 moveInput);
    public event OnMoveAction onMove;

    public delegate void OnLookAction(Vector2 lookInput);
    public event OnLookAction onLook;

    public delegate void OnJumpAction();
    public event OnJumpAction onJump;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Jump.performed += HandleJumpInput;
        inputActions.Player.Blink.performed += HandleBlinkInput;
        inputActions.Player.Blink.canceled += HandleBlinkInput;

        inputActions.Player.Pause.performed += HandlePauseInput;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();

        inputActions.Player.Jump.performed -= HandleJumpInput;
        inputActions.Player.Blink.performed -= HandleBlinkInput;
        inputActions.Player.Blink.canceled -= HandleBlinkInput;
    }



    private void Update()
    {
        HandleMoveInput();

        HandleLookInput();
    }

    private void HandleMoveInput()
    {
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        onMove?.Invoke(moveInput);
    }

    private void HandleLookInput()
    {
        Vector2 lookInput = inputActions.Player.Look.ReadValue<Vector2>();
        onLook?.Invoke(lookInput);
    }

    private void HandleBlinkInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            BlinkEvents.TriggerBlink();
        }
        else if(context.canceled)
        {
            BlinkEvents.TriggerBlinkRelease();
        }
    }

    private void HandleJumpInput(InputAction.CallbackContext context)
    {
        onJump?.Invoke();
    }

    private void HandlePauseInput(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.IsGamePaused)
        {
            GameManager.Instance.ResumeGame();
        }
        else
        {
            GameManager.Instance.PauseGame();
        }
    }
}
