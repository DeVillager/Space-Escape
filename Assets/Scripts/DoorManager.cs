using System;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : Singleton<DoorManager>
{
    public List<Door> levelDoors;
    public List<Material> doorMaterials;
    public List<Material> switchMaterials;
    
    // public Dictionary<DoorColor, Material> doorMaterials;
    
    protected override void Awake()
    {
        base.Awake();
        levelDoors = new List<Door>();
    }

    public void AddDoor(Door door)
    {
        levelDoors.Add(door);
    }

    public void ToggleDoors(DoorColor color)
    {
        foreach (Door door in levelDoors)
        {
            if (door.color == color)
            {
                door.Toggle();
            }
        }
    }

    public void OpenDoorsForSeconds(DoorColor color, float time)
    {
        foreach (Door door in levelDoors)
        {
            if (door.color == color)
            {
                StartCoroutine(door.OpenForSeconds(time));
            }
        }
    }
}