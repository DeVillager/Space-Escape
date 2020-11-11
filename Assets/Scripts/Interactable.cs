using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
    public bool isGrabbable;
    public bool grabbed;
    public bool active = true;
    public Renderer rend;

    protected virtual void Awake()
    {
        // hintText = GetComponentInChildren<TextMeshProUGUI>();
        if (rend == null) rend = GetComponent<Renderer>();
        // rend.material = defaultMaterial;
    }

    public void ChangeDefaultMaterial()
    {
        ChangeMaterial(defaultMaterial);
    }

    public void ChangeHighlightMaterial()
    {
        ChangeMaterial(highlightMaterial);
    }

    private void ChangeMaterial(Material material)
    {
        rend.material = material;
        Renderer selectionRenderer = GetComponent<Renderer>();
        if (selectionRenderer != null)
        {
            selectionRenderer.material = material;
        }
    }

    // public void DisplayHintText(bool show)
    // {
    //     hintText.enabled = show;
    // }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }
    
}