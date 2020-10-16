using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Switch : Interactable
{

    public void ToggleDoor(GameObject door)
    {
        door.SetActive(!door.activeInHierarchy);
    }

}