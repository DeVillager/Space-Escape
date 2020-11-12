using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableManager : MonoBehaviour
{

    private PlayerInput input;

    private void Awake()
    {
        // _playerCamera = Camera.main;
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        // input = Player.Instance.controller.input;
        // input.Enable();
        // input.Player.Use.performed += HandleUse;
        // input.Player.Throw.started += HandleThrow;
        // input.Player.Use.started += HandleUse;
        // input.Player.Use.canceled += HandleRelease;
    }

    private void OnDisable()
    {

        // input.Player.Use.started -= HandleUse;
        // input.Player.Use.canceled -= HandleRelease;
    }



    
}