using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController charController;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Vector3 velocity;
    private bool _isGrounded;
    public bool isCrouching;
    private float standHeight;
    public float crouchHeight = 0.3f;
    public PlayerInput input;
    [SerializeField] private float startDropSpeed = -2f;

    private void Awake()
    {
        input = new PlayerInput();
        // charController = GetComponent<CharacterController>();
        charController = GetComponentInChildren<CharacterController>();
        standHeight = charController.height;
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Crouch.started += StartCrouch;
        input.Player.Crouch.canceled += EndCrouch;
        input.Player.Jump.started += StartJump;
    }

    private void StartJump(InputAction.CallbackContext obj)
    {
        if (_isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void StartCrouch(InputAction.CallbackContext obj)
    {
        Player.Instance.body.localScale = new Vector3(1, crouchHeight, 1);
    }

    private void EndCrouch(InputAction.CallbackContext obj)
    {
        Player.Instance.body.localScale = new Vector3(1, 1, 1);
    }
    
    private void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = startDropSpeed;
        }

        Vector2 v2 = input.Player.Move.ReadValue<Vector2>();
        Vector3 move = transform.right * v2.x + transform.forward * v2.y;

        charController.Move(move * (speed * Time.deltaTime));

        velocity.y += gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);
    }
}