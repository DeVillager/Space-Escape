using System;
using JetBrains.Annotations;
using UnityEngine;

public class Grabbable : Interactable
{
    [SerializeField] private Rigidbody rb;
    public bool grabbed;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    public void HandleGrabbing(Transform t)
    {
        if (!grabbed)
        {
            AttachToPlayer(t);
        }
        else
        {
            DetachFromPlayer();
        }
    }
    
    private void AttachToPlayer(Transform t)
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        var transform1 = transform;
        transform1.position = t.position;
        transform1.parent = t;
        grabbed = true;
    }

    private void DetachFromPlayer()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        transform.parent = null;
        grabbed = false;
    }

}