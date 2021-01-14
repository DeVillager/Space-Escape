using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("References")]
    public PlayerController controller;
    public Transform swayTarget;

    [Header("Settings")]
    public float verticalSwayMax = 0.05f;
    public float horizontalSwayMax = 0.1f;
    public float sensitivity = 1f;
    public float swaySpeed = 10f;

    // Privates
    private Vector3 origPos;

    public void Start()
    {
        origPos = swayTarget.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v2 = controller.input.Player.Rotate.ReadValue<Vector2>();

        float mouseX = v2.x;
        float mouseY = v2.y;

        Vector3 curPos = swayTarget.localPosition;
        Vector3 newPos = origPos;

        newPos.x = origPos.x + Mathf.Clamp(-mouseX * sensitivity * Time.deltaTime, -horizontalSwayMax, horizontalSwayMax);
        newPos.y = origPos.y + Mathf.Clamp(-mouseY * sensitivity * Time.deltaTime, -verticalSwayMax, verticalSwayMax);

        curPos = Vector3.Lerp(curPos, newPos, swaySpeed * Time.deltaTime);

        swayTarget.localPosition = curPos;
    }
}
