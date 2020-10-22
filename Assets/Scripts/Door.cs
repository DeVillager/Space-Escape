using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Door : MonoBehaviour
{
    public DoorColor color;
    public bool open = true;
    // private GameObject door;
    private Animator anim;
    private void Start()
    {
        DoorManager.Instance.AddDoor(this);
        anim = GetComponent<Animator>();
        // TODO Change to number materials
        // GetComponent<Renderer>().material = DoorManager.Instance.doorMaterials[(int) color];
    }

    public void Open()
    {
        open = true;
        anim.SetTrigger("Open");
        // gameObject.SetActive(!open);
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
        anim.SetTrigger("Close");
        // gameObject.SetActive(!open);
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