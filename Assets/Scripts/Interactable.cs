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
    public bool selected;

    protected virtual void Awake()
    {
        if (rend == null) rend = GetComponentInChildren<Renderer>();
        // rend.material = defaultMaterial;
    }

    public void ChangeDefaultMaterial()
    {
        if (rend.material != defaultMaterial)
        {
            ChangeMaterial(defaultMaterial);
        }
    }

    public void ChangeHighlightMaterial()
    {
        if (rend.material != highlightMaterial)
        {
            ChangeMaterial(!grabbed ? highlightMaterial : defaultMaterial);
        }
    }

    private void ChangeMaterial(Material material)
    {
        rend.material = material;
    }

    // public void DisplayHintText(bool show)
    // {
    //     hintText.enabled = show;
    // }

    public void Activate()
    {
        Debug.Log(gameObject + " activated");
        active = true;
    }

    public void Deactivate()
    {
        Debug.Log(gameObject + " deactivated");
        active = false;
    }
}