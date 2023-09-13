using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private CharacterControls characterControls;

    private Rigidbody2D rigidbody;

    private Vector2 movementDirection;

    private int numberOfJumps = 0;

    [HideInInspector] public int acornsCollected = 0;

    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxNumberOfJumps = 2;
    [SerializeField] private int numberOfAcornToWin = 4;

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
        CheckVictory();
    }


    private void JumpPlayer(InputAction.CallbackContext context)
    {
        bool isJumpPressed = context.ReadValueAsButton();

        if (isJumpPressed && numberOfJumps < maxNumberOfJumps)
        {
            rigidbody.AddForce(Vector2.up * jumpForce);
            numberOfJumps++;
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
    private void CheckVictory()
    {
        if(acornsCollected >= numberOfAcornToWin)
        {
            print("PARABÉNS! VOCÊ VENCEU!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        numberOfJumps = 0;
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
