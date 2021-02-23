using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventTrigger : MonoBehaviour
{
    public UnityEvent OnTriggerEnterEvent;
    public UnityEvent OnTriggerStayEvent;
    public UnityEvent OnTriggerExitEvent;

    private Collider _eventCollider;

    // public LayerMask mask;
    public bool enabled = true;


    protected virtual void Awake()
    {
        if (_eventCollider == null)
        {
            _eventCollider = GetComponent<Collider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && enabled)
        {
            OnTriggerEnterEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enabled)
        {
            OnTriggerStayEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enabled)
        {
            OnTriggerExitEvent.Invoke();
        }
    }
}