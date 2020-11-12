using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Options")]
    public float speed = 12f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float standingHeight = 1.25f;
    public float crouchingHeightMult = 0.5f;
    public float crouchingTime = 0.2f;
    public float headOffset = -0.15f;
    public float startDropSpeed = -2f;

    [Header("References")]
    public Camera headCamera;
    public Transform body;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Other")]
    public Vector3 velocity;
    public bool isCrouching;
    public PlayerInput input;
    private CharacterController charController;

    [Header("Interactable")]
    private Interactable _previousInteractable;
    public Interactable interactable;
    public Grabbable grabbable;
    public LayerMask selectionMask;
    public float selectionDistance = 3f;
    public GameObject grabbedObject;

    // Properties for ease of access and null checks
    private float crouchingHeight => standingHeight * crouchingHeightMult;
    private bool isGrounded => charController != null && charController.isGrounded;
    private float currentHeight => charController != null ? charController.height : standingHeight;

    private void Awake()
    {
        input = new PlayerInput();
        charController = GetComponent<CharacterController>();
        standingHeight = charController.height;
    }

    private void Start()
    {
        SetHeight(standingHeight);
    }
    
    private void Update()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = startDropSpeed;
        }

        UpdateHeight();

        Vector2 v2 = input.Player.Move.ReadValue<Vector2>();
        Vector3 move = transform.right * v2.x + transform.forward * v2.y;

        charController.Move(move * (speed * Time.deltaTime));

        velocity.y += gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);
        UpdateInteractables();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Crouch.started += StartCrouch;
        input.Player.Crouch.canceled += EndCrouch;
        input.Player.Jump.started += StartJump;
        input.Player.Use.performed += HandleUse;
        input.Player.Throw.started += HandleThrow;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Crouch.started -= StartCrouch;
        input.Player.Crouch.canceled -= EndCrouch;
        input.Player.Jump.started -= StartJump;
        input.Player.Use.performed -= HandleUse;
        input.Player.Throw.started -= HandleThrow;
    }

    private void StartJump(InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void StartCrouch(InputAction.CallbackContext obj)
    {
        isCrouching = true;
    }

    private void EndCrouch(InputAction.CallbackContext obj)
    {
        isCrouching = false;
    }
    
    private void HandleUse(InputAction.CallbackContext obj)
    {
        if (interactable != null && interactable.isGrabbable && interactable.grabbed)
        {
            // Grabbable g = interactable.GetComponent<Grabbable>();
            interactable.OnRelease.Invoke();
        }
        else if (interactable != null && interactable.active)
        {
            interactable.OnUse.Invoke();
        }
    }
    

    private void HandleThrow(InputAction.CallbackContext obj)
    {
        if (interactable != null && interactable.grabbed)
        {
            grabbable.OnThrow.Invoke();
        }
    }

    public void UpdateHeight()
    {
        float delta = ((standingHeight - crouchingHeight) * Time.deltaTime) / crouchingTime;
        float h = Mathf.Clamp(currentHeight + (isCrouching ? -delta : delta), crouchingHeight, standingHeight);
        SetHeight(h);
    }

    private void SetHeight(float h)
    {
        // Avoid unnecessary creation of new vector3s
        var v0 = new Vector3(0, h/2, 0);
        var v1 = new Vector3(1, h/2, 1);

        // Changes height of the collider
        charController.height = h;
        charController.center = v0;

        // Moves camera height
        headCamera.transform.localPosition = new Vector3(0, h - headOffset, 0); ;

        // This only scales visuals, nothing else
        body.localScale = v1;
        body.localPosition = v0;
    }
    
    private void UpdateInteractables()
    {
        // Handling selection of interactable which player is looking at
        Vector2 pos = new Vector2(Screen.width / 2, Screen.height / 2);
        var ray = headCamera.ScreenPointToRay(pos);
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, selectionDistance, selectionMask))
        {
            interactable = _hit.transform.gameObject.GetComponent<Interactable>();
            if (interactable.active)
            {
                if (interactable.isGrabbable)
                {
                    grabbable = interactable.GetComponent<Grabbable>();
                }

                interactable.OnSelect.Invoke();
            }

            //TODO Change handling to this if camera fast movement on interaction OnSelect becomes normal
            // if (interactable != _previousInteractable)
            // {
            //     if (_previousInteractable != null)
            //     {
            //         _previousInteractable.OnDeselect.Invoke();
            //         _previousInteractable = null;
            //     }
            //
            //     if (interactable.active)
            //     {
            //         if (interactable.isGrabbable)
            //         {
            //             grabbable = interactable.GetComponent<Grabbable>();
            //         }
            //
            //         interactable.OnSelect.Invoke();
            //         _previousInteractable = interactable;
            //     }
            // }
        }
        else
        {
            if (interactable != null)
            {
                interactable.OnDeselect.Invoke();
                interactable = null;
            }
        }
    }
}