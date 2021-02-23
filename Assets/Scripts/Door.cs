using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class Door : MonoBehaviour
{
    public bool open = false;
    private Animator anim;
    public AudioSource audioSource;

    private void Start()
    {
        DoorManager.Instance.AddDoor(this);
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator OpenForSeconds(float time)
    {
        Open();
        yield return new WaitForSeconds(time);
        Close();
    }

    public void Open()
    {
        if (!open)
        {
            open = true;
            audioSource.Play();
            anim.SetTrigger("Open");
        }
    }

    public void Close()
    {
        if (open)
        {
            open = false;
            audioSource.Play();
            anim.SetTrigger("Close");
        }
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