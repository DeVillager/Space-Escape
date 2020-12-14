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
}