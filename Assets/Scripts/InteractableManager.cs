using System;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    // [SerializeField] private string selectableTag = "Selectable";
    // [SerializeField] private Material defaultMaterial;

    // [SerializeField] private Material highlightMaterial;

    // public Transform _selection;
    public Interactable interactable;
    private Camera _playerCamera;
    public LayerMask selectionMask;

    public float selectionDistance = 3f;

    // private RaycastHit _hit;
    public GameObject grabbedObject;

    private void Awake()
    {
        _playerCamera = Camera.main;
    }

    private void Update()
    {
        //Use handling
        if (Input.GetButton("Fire1") && interactable != null)
        {
            if (interactable.isGrabbable)
            {
                interactable.OnHold.Invoke();
            }
            else if (Input.GetButtonDown("Fire1") && interactable != null)
            {
                interactable.OnUse.Invoke();
            }
        }


        var ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, selectionDistance, selectionMask))
        {
            // Debug.Log($"Hit {_hit.transform.gameObject}");
            interactable = _hit.transform.gameObject.GetComponent<Interactable>();
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