using System;
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
        body = transform.GetChild(0);
    }
    
}