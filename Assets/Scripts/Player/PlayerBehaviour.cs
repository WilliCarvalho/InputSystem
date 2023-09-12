using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private CharacterControls characterControls;

    private Rigidbody2D rigidbody;
    private Vector2 movementDirection;

    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        characterControls = new CharacterControls();

        characterControls.Movement.Move.started += ReceiveMovePlayerInput;
        characterControls.Movement.Move.performed += ReceiveMovePlayerInput;
        characterControls.Movement.Move.canceled += ReceiveMovePlayerInput;

        characterControls.Movement.Jump.started += JumpPlayer;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void JumpPlayer(InputAction.CallbackContext context)
    {
        bool isJumpPressed = context.ReadValueAsButton();

        if (isJumpPressed)
        {
            rigidbody.AddForce(Vector2.up * jumpForce);
        }
    }

    private void MovePlayer()
    {
        transform.Translate(movementDirection * velocity * Time.deltaTime);
    }

    private void ReceiveMovePlayerInput(InputAction.CallbackContext context)
    {
        movementDirection.x = context.ReadValue<float>();
    }

    private void OnEnable()
    {
        characterControls.Enable();
    }

    private void OnDisable()
    {
        characterControls.Disable();
        characterControls.Movement.Move.started -= ReceiveMovePlayerInput;
        characterControls.Movement.Move.performed -= ReceiveMovePlayerInput;
        characterControls.Movement.Move.canceled -= ReceiveMovePlayerInput;
    }
}
