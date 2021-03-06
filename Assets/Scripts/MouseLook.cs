﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 10f;
    public Transform playerBody;
    private float _xRotation = 0f;
    private float hiddenScalar = 4f;

    private PlayerInput input;

    [SerializeField] private float rotateTime = 0.5f;
    [SerializeField] private float time = 0f;
    private Vector2 v1;
    // private Quaternion q;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        input = Player.Instance.controller.input;
        // q = transform.localRotation;
    }

    void Update()
    {
        //TODO Make rotation to smooth lerp
        // var mousePosition = input.Player.MousePosition.ReadValue<Vector2>();
        // var projectedMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        if (UIManager.Instance != null && !UIManager.Instance.gamePaused)
        {
            Vector2 v2 = input.Player.Rotate.ReadValue<Vector2>();

            float mouseX = v2.x * mouseSensitivity * Time.fixedDeltaTime * hiddenScalar;
            float mouseY = v2.y * mouseSensitivity * Time.fixedDeltaTime * hiddenScalar;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

            // testing lerping rotation, dosent work :(
            // if (Mathf.Abs(Quaternion.Dot(transform.localRotation, q)) < 0.999999f)
            // {
            //     time += Time.deltaTime;
            //     transform.localRotation = Quaternion.Lerp(transform.localRotation, q, time / rotateTime);
            // }
            // else
            // {
            //     q = Quaternion.Euler(_xRotation, 0f, 0f);
            //     time = 0;
            // }
        }
    }
}

// using System;
// using UnityEngine;
//
//     [Serializable]
//     public class MouseLook : MonoBehaviour
//     {
//         public float XSensitivity = 2f;
//         public float YSensitivity = 2f;
//         public bool clampVerticalRotation = true;
//         public float MinimumX = -90F;
//         public float MaximumX = 90F;
//         public bool smooth;
//         public float smoothTime = 5f;
//         public bool lockCursor = true;
//         
//         public PlayerInput input;
//
//  
//         private Quaternion m_CharacterTargetRot;
//         private Quaternion m_CameraTargetRot;
//         private bool m_cursorIsLocked = true;
//  
//         public void Init(Transform character, Transform camera)
//         {
//             m_CharacterTargetRot = character.localRotation;
//             m_CameraTargetRot = camera.localRotation;
//         }
//         
//         private void Awake()
//         {
//             input = new PlayerInput();
//         }
//
//         private void OnEnable()
//         {
//             input.Enable();
//         }
//
//         private void OnDisable()
//         {
//             input.Disable();
//         }
//
//  
//         public void LookRotation(Transform character, Transform camera)
//         {
//             Vector2 v2 = input.Player.Rotate.ReadValue<Vector2>();
//             float xRot = v2.x * XSensitivity;
//             float yRot = v2.y * YSensitivity;
//             // float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;
//  
//             m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
//             m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);
//  
//             if(clampVerticalRotation)
//                 m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);
//  
//             if(smooth)
//             {
//                 character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
//                     smoothTime * Time.deltaTime);
//                 camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
//                     smoothTime * Time.deltaTime);
//             }
//             else
//             {
//                 character.localRotation = m_CharacterTargetRot;
//                 camera.localRotation = m_CameraTargetRot;
//             }
//  
//             UpdateCursorLock();
//         }
//  
//         public void SetCursorLock(bool value)
//         {
//             lockCursor = value;
//             if(!lockCursor)
//             {//we force unlock the cursor if the user disable the cursor locking helper
//                 Cursor.lockState = CursorLockMode.None;
//                 Cursor.visible = true;
//             }
//         }
//  
//         public void UpdateCursorLock()
//         {
//             //if the user set "lockCursor" we check & properly lock the cursos
//             if (lockCursor)
//                 InternalLockUpdate();
//         }
//  
//         private void InternalLockUpdate()
//         {
//             if(Input.GetMouseButtonUp(0))
//             {
//                 m_cursorIsLocked = true;
//             }
//  
//             if (m_cursorIsLocked)
//             {
//                 Cursor.lockState = CursorLockMode.Locked;
//                 Cursor.visible = false;
//             }
//             else if (!m_cursorIsLocked)
//             {
//                 Cursor.lockState = CursorLockMode.None;
//                 Cursor.visible = true;
//             }
//         }
//  
//         Quaternion ClampRotationAroundXAxis(Quaternion q)
//         {
//             q.x /= q.w;
//             q.y /= q.w;
//             q.z /= q.w;
//             q.w = 1.0f;
//  
//             float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);
//  
//             angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);
//  
//             q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);
//  
//             return q;
//         }
//  
//     }