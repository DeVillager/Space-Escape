using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI hintText;
    // [SerializeField] private Material defaultMaterial;
    // [SerializeField] private Material highlightMaterial;
    
    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;
    public UnityEvent OnUse;
    public UnityEvent OnHold;

    public bool isGrabbable;

    
    protected virtual void Awake()
    {
        hintText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // private void OnEnable()
    // {
    //     OnSelect += ChangeMaterial(highlightMaterial);
    // }

    public void ChangeMaterial(Material material)
    {
        Renderer selectionRenderer = GetComponent<Renderer>();
        if (selectionRenderer != null)
        {
            selectionRenderer.material = material;
        }
    }

    public void DisplayHintText(bool show)
    {
        hintText.enabled = show;
    }
    
    // public abstract void OnSelect();
    // public abstract void OnDeselect();
    // public abstract void Use();
}