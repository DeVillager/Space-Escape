using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using InteractableTypes;
using UnityEditor;

public abstract class Interactable : MonoBehaviour
{
    // private TextMeshProUGUI hintText;
    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;
    public UnityEvent OnUse;
    public UnityEvent OnHold;
    public UnityEvent OnRelease;
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    public Material defaultMaterial;
    public Material highlightMaterial;
    public Material disabledMaterial;
    public Material activatedMaterial;

    public bool isGrabbable;
    public Renderer rend;

    private State _itemState = State.active;

    public State ItemState
    {
        get => _itemState;
        set
        {
            if (_itemState == value) return;
            _itemState = value;
            OnStateChange?.Invoke(_itemState);
        }
    }

    public delegate void OnVariableChangeDelegate(State newState);

    public event OnVariableChangeDelegate OnStateChange;


    private void StateChangeHandler(State newState)
    {
        switch (newState)
        {
            case State.active:
                ChangeDefaultMaterial();
                break;
            case State.inactive:
                ChangeDisabledMaterial();
                break;
            case State.selected:
                ChangeHighlightMaterial();
                break;
            case State.grabbed:
                ChangeDefaultMaterial();
                break;
            case State.activated:
                ChangeActivatedMaterial();
                break;
        }
    }

    protected virtual void Awake()
    {
        OnStateChange += StateChangeHandler;
        if (rend == null) rend = GetComponentInChildren<Renderer>();
        if (ItemState == State.active)
        {
            ChangeDefaultMaterial();
        }
        else
        {
            ChangeDisabledMaterial();
        }
    }

    private void OnEnable()
    {
        OnSelect.AddListener(ChangeHighlightMaterial);
        OnDeselect.AddListener(ChangeDefaultMaterial);
        OnUse.AddListener(ChangeActivatedMaterial);
        OnUse.AddListener(ChangeActivatedMaterial);
        OnActivate.AddListener(ChangeDefaultMaterial);
        OnDeactivate.AddListener(ChangeDisabledMaterial);
    }

    private void OnDisable()
    {
        OnSelect.RemoveListener(ChangeHighlightMaterial);
        OnDeselect.RemoveListener(ChangeDefaultMaterial);
        OnUse.RemoveListener(ChangeActivatedMaterial);
        OnActivate.RemoveListener(ChangeDefaultMaterial);
        OnDeactivate.RemoveListener(ChangeDisabledMaterial);
    }

    public void ChangeDefaultMaterial()
    {
        ChangeMaterial(defaultMaterial);
    }

    public void ChangeHighlightMaterial()
    {
        // ChangeMaterial(ItemState != State.grabbed ? highlightMaterial : defaultMaterial);
        ChangeMaterial(highlightMaterial);
    }

    public void ChangeDisabledMaterial()
    {
        ChangeMaterial(disabledMaterial);
    }

    public void ChangeActivatedMaterial()
    {
        Debug.Log("jgkjlk");
        rend.material = activatedMaterial;
        // ChangeMaterial(activatedMaterial);
    }

    private void ChangeMaterial(Material material)
    {
        if (rend.material != material)
        {
            rend.material = material;
        }
    }

    private void Use()
    {
        if (ItemState == State.active)
        {
            ItemState = State.activated;
        }
        else if (ItemState == State.activated)
        {
            ItemState = State.active;
        }
    }

    public void Activate()
    {
        Debug.Log(gameObject + " activated");
        ItemState = State.active;
        ChangeDefaultMaterial();
    }

    public void Deactivate()
    {
        Debug.Log(gameObject + " deactivated");
        ItemState = State.inactive;
        ChangeDisabledMaterial();
    }
}