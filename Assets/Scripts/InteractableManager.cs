using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableManager : MonoBehaviour
{
    public Interactable interactable;
    public Grabbable grabbable;

    private Camera _playerCamera;
    public LayerMask selectionMask;

    public float selectionDistance = 3f;

    // private RaycastHit _hit;
    public GameObject grabbedObject;
    private PlayerInput input;

    private void Awake()
    {
        _playerCamera = Camera.main;
    }

    // private void Start()
    // {
    //     input = Player.Instance.controller.input;
    //     // input = new PlayerInput();
    // }

    private void OnEnable()
    {
        input = Player.Instance.controller.input;
        // input.Enable();
        // input.Enable();
        input.Player.Use.started += HandleUse;
        input.Player.Use.canceled += HandleRelease;
        input.Player.Throw.started += HandleThrow;
    }

    private void HandleThrow(InputAction.CallbackContext obj)
    {
        if (interactable != null && interactable.grabbed)
        {
            grabbable.OnThrow.Invoke();
        }
    }

    private void HandleRelease(InputAction.CallbackContext obj)
    {
        if (interactable != null && interactable.isGrabbable && interactable.grabbed)
        {
            // Grabbable g = interactable.GetComponent<Grabbable>();
            interactable.OnRelease.Invoke();
        }
    }

    private void HandleUse(InputAction.CallbackContext obj)
    {
        if (interactable != null)
        {
            interactable.OnUse.Invoke();
        }
    }


    private void Update()
    {
        //Throw Handling
        // if (Input.GetButtonDown("Fire2") && interactable.grabbed)
        // {
        //     grabbable.OnThrow.Invoke();
        // }

        //Use handling
        // if (interactable != null)
        // {
        //     if (Input.GetButtonDown("Fire1"))
        //     {
        //         interactable.OnUse.Invoke();
        //     }
        //     else if (Input.GetButtonUp("Fire1") && interactable.isGrabbable)
        //     {
        //         // Grabbable g = interactable.GetComponent<Grabbable>();
        //         if (interactable.grabbed)
        //         {
        //             interactable.OnRelease.Invoke();
        //         }
        //     }
        // }
        // var ray = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        // ReSharper disable once PossibleLossOfFraction
        Vector2 pos = new Vector2(Screen.width / 2 , Screen.height / 2);
        // Debug.Log(input.Player.MousePosition.ReadValue<Vector2>().ToString());
        // var ray = _playerCamera.ScreenPointToRay(input.Player.MousePosition.ReadValue<Vector2>());
        var ray = _playerCamera.ScreenPointToRay(pos);
        // var ray = _playerCamera.ScreenToWorldPoint(Mouse.current.position);
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, selectionDistance, selectionMask))
        {
            // Debug.Log($"Hit {_hit.transform.gameObject}");
            interactable = _hit.transform.gameObject.GetComponent<Interactable>();
            if (interactable.isGrabbable)
            {
                grabbable = interactable.GetComponent<Grabbable>();
            }

            // interactable.OnSelect();
            interactable.OnSelect.Invoke();
        }
        else
        {
            if (interactable != null)
            {
                interactable.OnDeselect.Invoke();
                // interactable.OnDeselect();
                interactable = null;
            }
        }
    }
}