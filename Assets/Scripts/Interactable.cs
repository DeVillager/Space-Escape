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
    public UnityEvent OnActivation;
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    public Material defaultMaterial;
    public Material highlightMaterial;
    public Material disabledMaterial;
    public Material activatedMaterial;

    public bool isGrabbable;
    public bool isTogglable;
    public Renderer rend;

    [SerializeField] private State itemState = State.active;

    public State ItemState
    {
        get => itemState;
        set
        {
            if (itemState == value) return;
            itemState = value;
            OnStateChange?.Invoke(itemState);
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
                OnSelect.Invoke();
                break;
            case State.grabbed:
                OnHold.Invoke();
                break;
            case State.activated:
                OnActivation.Invoke();
                break;
        }
    }

    protected virtual void Awake()
    {
        OnStateChange += StateChangeHandler;
        if (rend == null) rend = GetComponentInChildren<Renderer>();
        StateChangeHandler(ItemState);
    }

    // todo: make functions for listeners which call all other delegates functions
    private void OnEnable()
    {
        OnSelect.AddListener(ChangeHighlightMaterial);
        OnDeselect.AddListener(ChangeDefaultMaterial);
        OnHold.AddListener(ChangeDefaultMaterial);
        OnUse.AddListener(Use);
        OnActivation.AddListener(ActivateItem);
        OnActivate.AddListener(Activate);
        OnDeactivate.AddListener(Deactivate);
    }

    private void OnDisable()
    {
        OnSelect.RemoveListener(ChangeHighlightMaterial);
        OnDeselect.RemoveListener(ChangeDefaultMaterial);
        OnHold.AddListener(ChangeDefaultMaterial);
        OnUse.RemoveListener(Use);
        OnActivation.AddListener(ActivateItem);
        OnActivate.RemoveListener(Activate);
        OnDeactivate.RemoveListener(Deactivate);
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
        ChangeMaterial(activatedMaterial);
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
        if (ItemState == State.selected)
        {
            ItemState = State.activated;
        }
        else if (isTogglable && ItemState == State.activated)
        {
            ItemState = State.selected;
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

    private void ActivateItem()
    {
        ChangeActivatedMaterial();
    }
}