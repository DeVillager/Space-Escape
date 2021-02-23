using System;
using System.Collections;
using System.Collections.Generic;
using InteractableTypes;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorSwitch : Interactable
{
    public List<Door> doors;
    
    public AudioSource audiosource;
    public AudioClip clip;
    public AudioClip declineCLip;

    protected override void Awake()
    {
        base.Awake();
        audiosource = GetComponent<AudioSource>();
    }

    public void ToggleDoor(Door door)
    {
        door.Toggle();
    }

    public void OpenDoors()
    {
        audiosource.PlayOneShot(clip);
        foreach (Door door in doors)
        {
            door.Toggle();
        }
    }
    
    public void OpenDoorsPermanently()
    {
        audiosource.PlayOneShot(clip);
        foreach (Door door in doors)
        {
            door.Open();
        }
    }

    public void OpenDoorsForSeconds(float time)
    {
        audiosource.PlayOneShot(clip);
        foreach (Door door in doors)
        {
            StartCoroutine(door.OpenForSeconds(time));
            StartCoroutine(ActivatedForSeconds(time));
        }
    }

    public IEnumerator ActivatedForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        audiosource.PlayOneShot(declineCLip);
        ItemState = State.active;
    }

    public void ActivateInteractable(Interactable i)
    {
        i.OnActivate.Invoke();
    }
}