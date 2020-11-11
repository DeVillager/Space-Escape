using System;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : Interactable
{
    public List<Door> doors;

    public void ToggleDoor(Door door)
    {
        door.Toggle();
    }

    public void OpenDoors()
    {
        foreach (Door door in doors)
        {
            door.Toggle();
        }
    }
    
    public void OpenDoorsPermanently()
    {
        foreach (Door door in doors)
        {
            door.Open();
        }
    }

    public void OpenDoorsForSeconds(float time)
    {
        foreach (Door door in doors)
        {
            StartCoroutine(door.OpenForSeconds(time));
        }
    }
    
    public void ActivateInteractable(Interactable i)
    {
        i.OnActivate.Invoke();
    }
}