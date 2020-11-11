using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableManager : MonoBehaviour
{
    private Interactable _previousInteractable;
    public Interactable interactable;
    public Grabbable grabbable;

    private Camera _playerCamera;
    public LayerMask selectionMask;

    public float selectionDistance = 3f;

    public GameObject grabbedObject;
    private PlayerInput input;

    private void Awake()
    {
        _playerCamera = Camera.main;
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        // input = Player.Instance.controller.input;
        input.Enable();
        input.Player.Use.performed += HandleUse;
        input.Player.Throw.started += HandleThrow;
        // input.Player.Use.started += HandleUse;
        // input.Player.Use.canceled += HandleRelease;
    }

    private void OnDisable()
    {
        input.Player.Use.performed -= HandleUse;
        input.Player.Throw.started -= HandleThrow;
        // input.Player.Use.started -= HandleUse;
        // input.Player.Use.canceled -= HandleRelease;
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

    // private void HandleRelease(InputAction.CallbackContext obj)
    // {
    //     if (interactable != null && interactable.isGrabbable && interactable.grabbed)
    //     {
    //         // Grabbable g = interactable.GetComponent<Grabbable>();
    //         interactable.OnRelease.Invoke();
    //     }
    // }

    private void HandleThrow(InputAction.CallbackContext obj)
    {
        if (interactable != null && interactable.grabbed)
        {
            grabbable.OnThrow.Invoke();
        }
    }

    private void Update()
    {
        // Handling selection of interactable which player is looking at
        Vector2 pos = new Vector2(Screen.width / 2, Screen.height / 2);
        var ray = _playerCamera.ScreenPointToRay(pos);
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