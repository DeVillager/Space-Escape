﻿using InteractableTypes;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : Interactable
{
    public UnityEvent OnThrow;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private float throwSpeed = 500f;

    private float minSpeed = 0f;
    private float maxSpeed = 600f;
    private float maxSpeedDistance = 3f;
    private float dropDistance = 5f;
    private float rotationSpeed = 6f;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (ItemState == State.grabbed)
        {
            float distance = Vector3.Distance(Player.Instance.grabPoint.position, transform.position);

            float currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, distance / maxSpeedDistance);
            currentSpeed *= Time.fixedDeltaTime;

            Vector3 direction = Player.Instance.grabPoint.position - transform.position;
            rb.velocity = direction.normalized * currentSpeed;

            Quaternion lookRot = Quaternion.LookRotation(Player.Instance.CameraPosition - transform.position);
            lookRot = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(lookRot);

            if (distance > dropDistance)
                DetachFromPlayer();
        }
    }

    public void AttachToPlayer()
    {
        ItemState = State.grabbed;
        Player.Instance.controller.grabbedObject = this;
    }

    public void DetachFromPlayer()
    {
        ItemState = State.active;
        Player.Instance.controller.grabbedObject = null;
    }

    public void Throw()
    {
        DetachFromPlayer();
        Vector2 pos = new Vector2(Screen.width / 2 , Screen.height / 2);
        var ray = Camera.main.ScreenPointToRay(pos);
        rb.AddForce(ray.direction.normalized * throwSpeed);
    }

}