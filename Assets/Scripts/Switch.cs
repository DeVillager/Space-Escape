using System;
using UnityEngine;

public class Switch : Interactable
{
    public DoorColor color;
    

    private void Start()
    {
        defaultMaterial = DoorManager.Instance.switchMaterials[(int) color];
        GetComponent<Renderer>().material = defaultMaterial;
    }

    public void ToggleDoor(GameObject door)
    {
        door.SetActive(!door.activeInHierarchy);
    }

    public void OpenDoors()
    {
        DoorManager.Instance.ToggleDoors(color);
    }
    
    public void OpenDoorForSeconds(float time)
    {
        DoorManager.Instance.OpenDoorsForSeconds(color, time);
    }

}