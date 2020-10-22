using System;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    public DoorColor color;
    public List<Door> doors;

    private void Start()
    {
        // defaultMaterial = DoorManager.Instance.switchMaterials[(int) color];
        // rend.material = defaultMaterial;
    }

    public void ToggleDoor(Door door)
    {
        door.Toggle();
    }

    public void OpenNumberDoors()
    {
        DoorManager.Instance.ToggleDoors(color);
    }

    public void OpenNumberDoorsForSeconds(float time)
    {
        DoorManager.Instance.OpenDoorsForSeconds(color, time);
    }

    public void OpenDoors()
    {
        foreach (Door door in doors)
        {
            door.Toggle();
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