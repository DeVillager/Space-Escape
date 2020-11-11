using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 10f;
    public Transform playerBody;
    private float _xRotation = 0f;
    private PlayerInput input;
    // private Vector2 v2;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        input = Player.Instance.controller.input;
    }

    // private void OnEnable()
    // {
    //     input.Player.MousePosition.performed += c => v2 = c.ReadValue<Vector2>();
    // }

    void Update()
    {
        //TODO Make rotation to smooth lerp
        // var mousePosition = input.Player.MousePosition.ReadValue<Vector2>();
        // var projectedMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Vector2 v2 = input.Player.Rotate.ReadValue<Vector2>();
        // Debug.Log(v2.ToString());
        // Vector2 v2 = Camera.main.ScreenToWorldPoint(mousePosition);
        float mouseX =  v2.x * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = v2.y * mouseSensitivity * Time.fixedDeltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}