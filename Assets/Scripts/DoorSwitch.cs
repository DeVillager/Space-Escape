using System;
using System.Collections;
using System.Collections.Generic;
using InteractableTypes;
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
            StartCoroutine(ActivatedForSeconds(time));
        }
    }

    public IEnumerator ActivatedForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        ItemState = State.active;
    }

    public void ActivateInteractable(Interactable i)
    {
        i.OnActivate.Invoke();
    }
}