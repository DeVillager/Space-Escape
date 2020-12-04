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

    protected virtual void Awake()
    {
        if (_eventCollider == null)
        {
            _eventCollider = GetComponent<Collider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // if (((1 << other.gameObject.layer) & mask) != 0)
            // {
            // }
            OnTriggerEnterEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent.Invoke();
    }
}