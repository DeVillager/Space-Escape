using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutomaticDoor : Door
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Close();
        }
    }

}