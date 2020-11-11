using System;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    public void ActivateInteractable(Interactable i)
    {
        i.OnActivate.Invoke();
    }
}