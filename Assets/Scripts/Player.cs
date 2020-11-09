﻿using System;
using UnityEngine;

public class Player: Singleton<Player>
{
    [HideInInspector]
    public PlayerController controller;
    public Transform grabPoint;
    public Transform body;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();
    }

    public bool IsHoldingItem()
    {
        return grabPoint.childCount > 0;
    }

    public Grabbable HeldItem()
    {
        if (IsHoldingItem())
        {
            return grabPoint.GetChild(0).GetComponent<Grabbable>();
        }

        return null;
    }
    
}