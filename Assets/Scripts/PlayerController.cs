using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Options")]
    public float speed = 12f;
    public float crouchSpeedMult = 0.5f;
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
    public LayerMask headCollisionMask;

    [Header("Other")]
    public Vector3 velocity;
    public bool isCrouching;
    public PlayerInput input;

    [Header("Interactable")]
    private Interactable _previousInteractable;
    public Interactable interactable;
    public Grabbable grabbable;
    public LayerMask selectionMask;
    public float selectionDistance = 3f;

    // Private variables
    private CharacterController charController;
    private float headCollisionThreshold = 0.02f;
    public Grabbable grabbedObject;

    // Properties for ease of access and null checks
    private float crouchingHeight => standingHeight * crouchingHeightMult;
    private bool isGrounded => charController.isGrounded;
    private float currentHeight => charController.height + (charController.skinWidth * 2);

    private void Awake()
    {
        input = new PlayerInput();
        charController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        SetHeight(standingHeight);
    }
    
    private void Update()
    {
        if (isGrounded && velocity.y < 0)
            velocity.y = startDropSpeed;

        if (!isGrounded && velocity.y > 0 && CheckHeadCollision())
            velocity.y = 0f;

        UpdateHeight();

        Vector2 v2 = input.Player.Move.ReadValue<Vector2>();
        Vector3 move = transform.right * v2.x + transform.forward * v2.y;

        charController.Move(move * (speed * Time.deltaTime) * (isCrouching ? crouchSpeedMult : 1f));

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
        h -= charController.skinWidth * 2;

        // Avoid unnecessary creation of new vector3s
        var bodyPosition = new Vector3(0, h/2, 0);
        var bodyScale = new Vector3(charController.radius * 2, h/2, charController.radius * 2);

        // Changes height of the collider
        charController.height = h;
        charController.center = bodyPosition;

        // Moves camera height
        headCamera.transform.localPosition = new Vector3(0, h + headOffset, 0); ;

        // This only scales visuals, nothing else
        body.localScale = bodyScale;
        body.localPosition = bodyPosition;
    }
    
    private void UpdateInteractables()
    {
        // Handling selection of interactable which player is looking at
        Vector2 pos = new Vector2(Screen.width / 2, Screen.height / 2);
        var ray = headCamera.ScreenPointToRay(pos);
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, selectionDistance, selectionMask))
        {
            // interactable = _hit.transform.gameObject.GetComponent<Interactable>();
            // Interactable hits collider within model object. Interactable script is in parent of model object.
            interactable = _hit.transform.gameObject.GetComponentInParent<Interactable>();
            if (interactable.active)
            {
                interactable.OnSelect.Invoke();
                if (interactable.isGrabbable)
                {
                    grabbable = interactable.GetComponentInParent<Grabbable>();
                }
            }
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

    private bool CheckHeadCollision()
    {
        float checkRadius = charController.radius - 0.1f;
        Vector3 checkPosition = transform.position + new Vector3(0f, currentHeight - checkRadius + headCollisionThreshold, 0f);

        return Physics.CheckSphere(checkPosition, checkRadius, headCollisionMask, QueryTriggerInteraction.Ignore);
    }
}