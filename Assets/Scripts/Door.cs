using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Door : MonoBehaviour
{
    public DoorColor color;
    public bool open = true;
    // private GameObject door;
    
    private void Start()
    {
        DoorManager.Instance.AddDoor(this);
        GetComponent<Renderer>().material = DoorManager.Instance.doorMaterials[(int) color];
    }

    public void Open()
    {
        open = true;
        gameObject.SetActive(!open);
    }

    public IEnumerator OpenForSeconds(float time)
    {
        Open();
        yield return new WaitForSeconds(time);
        Close();
    }

    public void Close()
    {
        open = false;
        gameObject.SetActive(!open);
    }

    public void Toggle()
    {
        if (!open)
        {
            Open();
        }
        else
        {
            Close();
        }
    }
}