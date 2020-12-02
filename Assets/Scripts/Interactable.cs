﻿using System;
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
        if (rend == null) rend = GetComponentInChildren<Renderer>();
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
        // Debug.Log("Changed material to " + material.name);
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