using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private CharacterControls characterControls;

    private Vector2 movementDirection;

    [SerializeField] private float velocity;

    private void Awake()
    {
        characterControls = new CharacterControls();

        characterControls.Movement.Move.started += ReceivePlayerInput;
        characterControls.Movement.Move.performed += ReceivePlayerInput;
        characterControls.Movement.Move.canceled += ReceivePlayerInput;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        transform.Translate(movementDirection * velocity * Time.deltaTime);
    }

    private void ReceivePlayerInput(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        characterControls.Enable();
    }

    private void OnDisable()
    {
        characterControls.Disable();
        characterControls.Movement.Move.started -= ReceivePlayerInput;
        characterControls.Movement.Move.performed -= ReceivePlayerInput;
        characterControls.Movement.Move.canceled -= ReceivePlayerInput;
    }
}
